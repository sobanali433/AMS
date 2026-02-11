using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AMS.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {


            //var p = _productRepository.GetAll();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> _Details(int? id)
        {


            return View();

        }

        [HttpPost]
        public JsonResult GetList()
        {
            //var data = _userRepository.GetAllUsersWithoutSuperAdmin();
            var user = _productRepository.GetList();

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
