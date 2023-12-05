using Microsoft.AspNetCore.Mvc;
using RecruitmentTask.Logic.Interfaces.Dtos;
using RecruitmentTask.Logic.Interfaces.Interfaces;
using RecruitmentTask.Logic.Interfaces.ServiceResponses;

namespace RecruitmentTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductLogic _productLogic;
        public ProductController(IProductLogic productLogic)
        {
            _productLogic = productLogic;
        }

        [HttpGet("get-csv-and-insert")]
        public async Task<ServiceResponse<bool>> GetCsvAndInsert()
        {
            var response = await _productLogic.DownloadAndSave();
            return response;
        }

        [HttpGet("get-product-by-sku")]
        public ServiceResponse<VProductionInfoDto> GetProductBySku(string sku)
        {
            return _productLogic.GetVproductinfo(sku);
        }
    }
}
