using CsvHelper.Configuration;
using RecruitmentTask.Database.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTask.Logic.Map
{
    public class InventoryCsvMapper : ClassMap<Inventory>
    {
        public InventoryCsvMapper()
        {
            Map(m => m.Id).Name("product_id");
            Map(m => m.Sku).Name("sku");
            Map(m => m.Quantity).Name("qty");
            Map(m => m.Unit).Name("unit");
            Map(m => m.Shippincost).Name("shipping_cost");
            Map(m => m.Shipping).Name("shipping");
        }
    }
}
