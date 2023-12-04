using RecruitmentTask.Database.Persistence.Models;
using RecruitmentTask.Logic.Dtos;
using RecruitmentTask.Logic.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RecruitmentTask.Logic.Logics
{
    public class ProductLogic : IProductLogic
    {
        private readonly string productsHttp = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv";
        private readonly string inventoryHttp = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv";
        private readonly string pricesHttp = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv";
        private readonly RecruitmentTaskBaseContext _context;
        public List<FileNames> FileNames { get; set; }

        public ProductLogic(RecruitmentTaskBaseContext context)
        {
            _context = context;
        }

        public async Task DownloadAndSave()
        {
            CreateNames();

            foreach(var file in FileNames)
            {
                byte[] data = await DownloadCsv(file.Url);
                await SaveCsv(file.Name, data);
            }
        }

        private void CreateNames()
        {
            List<string> urls = new List<string>();
            FileNames = new List<FileNames>();

            urls.Add(inventoryHttp);
            urls.Add(productsHttp);
            urls.Add(pricesHttp);

            foreach(string name in urls)
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "RecruitmentTask.Files", "Files", name + ".csv");
            await File.WriteAllBytesAsync(filePath, data);
        }

        private async Task<byte[]> DownloadCsv(string url)
        {
            using(HttpClient httpClient = new())
            {
                byte[] data = await httpClient.GetByteArrayAsync(url);
                return data;
            }
        }
    }
}
