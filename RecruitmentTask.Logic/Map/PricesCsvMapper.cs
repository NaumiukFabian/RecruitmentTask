using CsvHelper.Configuration;
using RecruitmentTask.Database.Persistence.Models;

namespace RecruitmentTask.Logic.Map
{
    public class PricesCsvMapper : ClassMap<Price>
    {
        public PricesCsvMapper()
        {
            Map(m => m.Id).Index(0);
            Map(m => m.Sku).Index(1);
            Map(m => m.Nettproductpice).Index(2);
        }
    }
}
