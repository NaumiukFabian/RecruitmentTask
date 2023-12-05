using CsvHelper.Configuration;
using RecruitmentTask.Database.Persistence.Models;


namespace RecruitmentTask.Logic.Map
{
    public class ProductCsvMapper : ClassMap<Product>
    {
        public ProductCsvMapper()
        {
            Map(m => m.Id).Name("ID");
            Map(m => m.Name).Name("name");
            Map(m => m.Sku).Name("SKU");
            Map(m => m.Ean).Name("EAN");
            Map(m => m.Productername).Name("producer_name");
            Map(m => m.Category).Name("category");
            Map(m => m.Shipping).Name("shipping");
            Map(m => m.Defaultimage).Name("default_image");
        }
    }
}
