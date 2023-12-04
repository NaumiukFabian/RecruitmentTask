using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTask.Logic.Interfaces.Dtos
{
    public class VProductionInfoDto
    {
        public string? Name { get; set; }

        public string? Ean { get; set; }

        public string? Productername { get; set; }

        public string? Defaultimage { get; set; }

        public decimal? Quantity { get; set; }

        public string? Unit { get; set; }

        public string? Shipping { get; set; }

        public decimal? Nettproductpice { get; set; }
    }
}
