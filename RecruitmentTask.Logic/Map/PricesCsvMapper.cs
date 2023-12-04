using CsvHelper.Configuration;
using RecruitmentTask.Database.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
