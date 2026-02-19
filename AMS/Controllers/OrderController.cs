using AMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Controllers
{
        public class OrderController : Controller
        {
            private readonly IOrderRepository _orderRepository;
            private int branchId = 0;
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
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var branchIdClaim = User.FindFirst("BranchId")?.Value;
            //var user = _orderRepository.GetAllWithDetailsAsync();



            if (!string.IsNullOrEmpty(branchIdClaim))
                branchId = int.Parse(branchIdClaim);
            var orders = await _orderRepository.GetOrdersByRoleAsync(role, branchId);

            var data = orders.Select(s => new
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
