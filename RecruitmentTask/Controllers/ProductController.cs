using Microsoft.AspNetCore.Mvc;
using RecruitmentTask.Logic.Dtos;
using RecruitmentTask.Logic.Interfaces.Dtos;
using RecruitmentTask.Logic.Interfaces.Interfaces;

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
        public async Task<IActionResult> GetCsvAndInsert()
        {
            await _productLogic.DownloadAndSave();
            return Ok();
        }

        [HttpGet("get-product-by-sku")]
        public VProductionInfoDto GetProductBySku(string sku)
        {
            return _productLogic.GetVproductinfo(sku);
        }
    }
}
