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
