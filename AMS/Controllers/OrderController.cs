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
                var orders =  _orderRepository.GetAllWithDetailsAsync();
                return View(orders);
            }




    }
}
