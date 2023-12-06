using CsvHelper;
using CsvHelper.Configuration;
using RecruitmentTask.Database.Persistence.Models;
using RecruitmentTask.Logic.Dtos;
using RecruitmentTask.Logic.Interfaces.Dtos;
using RecruitmentTask.Logic.Interfaces.Interfaces;
using RecruitmentTask.Logic.Interfaces.ServiceResponses;
using RecruitmentTask.Logic.Map;
using System.Globalization;

namespace RecruitmentTask.Logic.Logics
{
    public class ProductLogic : IProductLogic
    {
        private readonly string productsHttp = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv";
        private readonly string inventoryHttp = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv";
        private readonly string pricesHttp = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv";
        private readonly RecruitmentTaskBaseContext _context;
        public List<FileNames>? FileNames { get; set; }
        public string? BaseFilePath { get; set; }

        public ProductLogic(RecruitmentTaskBaseContext context)
        {
            _context = context;
        }

        public ServiceResponse<VProductionInfoDto> GetVproductinfo(string sku)
        {
            Vproductinfo? vproductinfo = _context.Vproductinfos.FirstOrDefault(x => x.Sku == sku);

            if (vproductinfo == null)
            {
                return new ServiceResponse<VProductionInfoDto> { Success = false, Message = "Nie znaleziono podanego SKU" };
            }

            VProductionInfoDto vProductInfoDto = new VProductionInfoDto()
            {
                NazwaProduktu = vproductinfo.NazwaProduktu,
                Ean = vproductinfo.Ean,
                NazwaProducenta = vproductinfo.NazwaProducenta,
                Kategoria = vproductinfo.Kategoria,
                Url = vproductinfo.Url,
                StanMagazynowy = vproductinfo.StanMagazynowy,
                JednostkaLogistyczna = vproductinfo.JednostkaLogistyczna,
                CenaNetto = vproductinfo.CenaNetto,
                KosztDostawy = vproductinfo.KosztDostawy,
            };

            return new ServiceResponse<VProductionInfoDto> { Data = vProductInfoDto, Success = true, Message = "Sukces" };
        }

        public async Task<ServiceResponse<bool>> DownloadAndSave()
        {
            try
            {
                CreateNames();
                if (FileNames == null)
                    return new ServiceResponse<bool> { Success = false, Message = "Zmieniono nazwę plików lub nie udało się wszystko pobrać" };

                foreach (var file in FileNames)
                {
                    byte[] data = await DownloadCsv(file.Url);
                    await SaveCsv(file.Name, data);
                }

                return ReadFiles();
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool> { Success = false, Message = ex.Message };
            }

        }

        private ServiceResponse<bool> ReadFiles()
        {
            if (FileNames == null)
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "Brak plików" };

            foreach (var file in FileNames)
            {
                if (BaseFilePath == null)
                    return new ServiceResponse<bool> { Data = false, Success = false, Message = "Brak wskazania lokalizacji plików" };

                string path = Path.Combine(BaseFilePath, file.Name + ".csv");

                using (var reader = new StreamReader(path))
                {
                    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        BadDataFound = null,
                        Delimiter = ";",
                        ShouldSkipRecord = record =>
                        {
                            return record.Row.GetField(0) == "__empty_line__";
                        }
                    };

                    if (file.Name == "Inventory" || file.Name == "Prices")
                    {
                        csvConfig.Delimiter = ",";
                    }

                    using (var csv = new CsvReader(reader, csvConfig))
                    {
                        var mapperFactory = new MapperFactory();
                        var csvMapper = mapperFactory.GetMapper(file.Name);
                        csv.Context.RegisterClassMap(csvMapper);

                        if (file.Name == "Products")
                        {
                            ProcessCsv<ProductCsvDto>(csv, r => r.Shipping == "24h" && (r.Category != null && !r.Category.Contains("Kable")));
                        }
                        else if (file.Name == "Inventory")
                        {
                            ProcessCsv<Inventory>(csv, r => r.Shipping == "24h");
                        }
                        else if (file.Name == "Prices")
                        {
                            ProcessCsv<Price>(csv, null);
                        }
                       
                    }

                }
            }

            return new ServiceResponse<bool> {Data = false, Success = true, Message = "Sukces" };
        }

        private void ProcessCsv<T>(CsvReader csv, Func<T, bool>? filter) where T : class
        {
            Type type = typeof(T);

            var records = csv.GetRecords<T>().ToList();
            if (filter != null)
            {
                var recordsToBase = records.Where(filter).ToList();

                if(type.FullName == "RecruitmentTask.Logic.Dtos.ProductCsvDto")
                {
                    MappingAndInserProducts(recordsToBase);
                    return;
                }

                SaveToDatabase(recordsToBase);

                return;
            }
            else
            {
                if (type.FullName == "RecruitmentTask.Logic.Dtos.ProductCsvDto")
                {
                    MappingAndInserProducts(records);
                    return;
                }

                SaveToDatabase(records);

                return;
            }
        }

        private void MappingAndInserProducts<T>(List<T> recordsToBase) where T : class
        {
            ProductCsvDtoToProduct productCsvDtoToProduct = new ProductCsvDtoToProduct();
            List<Product> recordsToBaseMapped = productCsvDtoToProduct.Map(recordsToBase as List<ProductCsvDto>);
            SaveToDatabase(recordsToBaseMapped);

            return;
        }

        private void SaveToDatabase<T>(List<T> recordsToBase) where T : class
        {
            _context.Set<T>().AddRange(recordsToBase);
            _context.SaveChanges();
        }

        private void CreateNames()
        {
            List<string> urls = new List<string>();
            FileNames = new List<FileNames>();

            urls.Add(inventoryHttp);
            urls.Add(productsHttp);
            urls.Add(pricesHttp);

            foreach (string name in urls)
            {
                FileNames fileNames = new FileNames()
                {
                    Name = name.Split('/').Last().Split('.').First(),
                    Url = name
                };

                FileNames.Add(fileNames);
            }
        }

        private async Task SaveCsv(string name, byte[] data)
        {
            BaseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "RecruitmentTask.Files", "Files");
            string filePath = Path.Combine(BaseFilePath, name + ".csv");
            await File.WriteAllBytesAsync(filePath, data);
        }

        private async Task<byte[]> DownloadCsv(string url)
        {
            using (HttpClient httpClient = new())
            {
                byte[] data = await httpClient.GetByteArrayAsync(url);
                return data;
            }
        }
    }
}
