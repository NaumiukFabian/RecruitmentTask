using CsvHelper.Configuration;


namespace RecruitmentTask.Logic.Map
{
    public class MapperFactory
    {
        public ClassMap GetMapper(string name)
        {
            if (name == "Products")
            {
                return new ProductCsvMapper();
            }
            else if (name == "Inventory")
            {
                return new InventoryCsvMapper();
            }
            else if (name == "Prices")
            {
                return new PricesCsvMapper();
            }
            // Dodaj więcej warunków dla innych plików CSV

            // Domyślne mapowanie
            return new InventoryCsvMapper();
        }
    }
}
