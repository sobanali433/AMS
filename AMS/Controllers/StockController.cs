using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace AMS.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        public StockController(IStockRepository stockRepository, IProductRepository productRepository, IUserRepository userRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public JsonResult GetList(int branchID)
        {
            //var data = _userRepository.GetAllUsersWithoutSuperAdmin();
            var user = _stockRepository.GetStockList(branchID);

            var result = new
            {
                draw = Request.Form["draw"].FirstOrDefault(),
                recordsTotal = user.Count,
                recordsFiltered = user.Count,
                data = user
            };

            return Json(result);
        }



    }
}
