using CsvHelper;
using CsvHelper.Configuration;
using RecruitmentTask.Database.Persistence.Models;
using RecruitmentTask.Logic.Dtos;
using RecruitmentTask.Logic.Interfaces.Dtos;
using RecruitmentTask.Logic.Interfaces.Interfaces;
using RecruitmentTask.Logic.Map;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public VProductionInfoDto GetVproductinfo(string sku)
        {
            Vproductinfo vproductinfo = _context.Vproductinfos.FirstOrDefault(x => x.Sku == sku);
            VProductionInfoDto vProductInfoDto = new VProductionInfoDto()
            {
                Name = vproductinfo.Name,
                Ean = vproductinfo.Ean,
                Productername = vproductinfo.Productername,
                Defaultimage = vproductinfo.Defaultimage,
                Quantity = vproductinfo.Quantity,
                Unit = vproductinfo.Unit,
                Shipping = vproductinfo.Shipping,
                Nettproductpice = vproductinfo.Nettproductpice,
            };

            return vProductInfoDto;
        }

        public async Task DownloadAndSave()
        {
            CreateNames();
            if (FileNames == null)
                return;

            foreach (var file in FileNames)
            {
                byte[] data = await DownloadCsv(file.Url);
                await SaveCsv(file.Name, data);
            }

            ReadFiles();
        }

        private void ReadFiles()
        {
            if (FileNames == null)
                return;

            foreach (var file in FileNames)
            {
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
                            var records = csv.GetRecords<Product>().ToList();
                            var recordsToBase = records.Where(r => r.Shipping == "24h" && (r.Category != null && !r.Category.Contains("Kable"))).ToList();
                            _context.Products.AddRange(recordsToBase);
                            _context.SaveChanges();
                        }
                        else if (file.Name == "Inventory")
                        {
                            var records = csv.GetRecords<Inventory>().ToList();
                            var recordsToBase = records.Where(r => r.Shipping == "24h").ToList();
                            _context.Inventories.AddRange(recordsToBase);
                            _context.SaveChanges();
                        }
                        else if(file.Name == "Prices")
                        {
                            var records = csv.GetRecords<Price>().ToList();
                            _context.Prices.AddRange(records);
                            _context.SaveChanges();
                        }
                    }
                }
            }
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
