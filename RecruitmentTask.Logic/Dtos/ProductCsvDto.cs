using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTask.Logic.Dtos
{
    public class ProductCsvDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Sku { get; set; }

        public string? Ean { get; set; }

        public string? Productername { get; set; }

        public string? Category { get; set; }

        public decimal? Iswire { get; set; }

        public decimal? Available { get; set; }

        public decimal? Isvendor { get; set; }

        public string? Defaultimage { get; set; }
        public string? Shipping {  get; set; }
    }
}
