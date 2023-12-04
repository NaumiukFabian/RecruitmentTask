using Microsoft.AspNetCore.Mvc;
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
    }
}
