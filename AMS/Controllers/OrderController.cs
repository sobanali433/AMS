using AMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Controllers
{
        public class OrderController : Controller
        {
            private readonly IOrderRepository _orderRepository;

            public OrderController(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public IActionResult Index()
            {
                return View();
            }

        [HttpPost]
        public async Task<JsonResult> GetList()
        {
            var user = _orderRepository.GetAllWithDetailsAsync();

            var data = user.Select(s => new
            {
                BranchName = s.BranchMasters.BranchName,
                ProductName = s.Products.ProductName,
                s.Quantity,
                s.OrderType,
                CreatedAt = s.CreatedAt.ToString("MM/dd/yyyy hh:mm tt")
                
            }).ToList();

            return Json(new
            {
                data = data
            });
        }


    }
}
