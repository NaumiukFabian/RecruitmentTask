using RecruitmentTask.Database.Persistence.Models;
using RecruitmentTask.Logic.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTask.Logic.Map
{
    public class ProductCsvDtoToProduct
    {
        public List<Product> Map(List<ProductCsvDto> productCsvDtos)
        {
            List<Product> result = new List<Product>();

            foreach(var productDto in productCsvDtos)
            {
                Product product = new Product()
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Sku = productDto.Sku,
                    Ean = productDto.Ean,
                    Productername = productDto.Productername,
                    Category = productDto.Category,
                    Iswire = productDto.Iswire,
                    Available = productDto.Available,
                    Isvendor = productDto.Isvendor,
                    Defaultimage = productDto.Defaultimage,

                };

                result.Add(product);
            }

            return result;
        }

        public Product Map(ProductCsvDto productCsvDto)
        {
            Product product = new Product()
            {
                Id = productCsvDto.Id,
                Name = productCsvDto.Name,
                Sku = productCsvDto.Sku,
                Ean = productCsvDto.Ean,
                Productername = productCsvDto.Productername,
                Category = productCsvDto.Category,
                Iswire = productCsvDto.Iswire,
                Available = productCsvDto.Available,
                Isvendor = productCsvDto.Isvendor,
                Defaultimage = productCsvDto.Defaultimage,

            };

            return product;
        }
    }
}
