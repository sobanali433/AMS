using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using System.Data;
using System.Threading.Tasks;

namespace AMS.Controllers
{
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IBranchRepository _branchRepository;
        public StockController(IStockRepository stockRepository, IProductRepository productRepository, IUserRepository userRepository, IOrderRepository orderRepository, IBranchRepository branchRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _branchRepository = branchRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Branches = _branchRepository.GetAllAsync();

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetList(int branchID)
        {
            //var data = _userRepository.GetAllUsersWithoutSuperAdmin();
            var user =await _stockRepository.GetStockListAsync(branchID);

            var data = user.Select(s => new
            {
                s.StockId,
                BranchName = s.BranchMasters.BranchName,
                ProductName = s.Products.ProductName,
                s.Quantity,
                LastUpdated = s.LastUpdated.ToString("MM/dd/yyyy hh:mm tt")
            }).ToList();

            return Json(new
            {
                data = data
            });
        }
        public IActionResult ManageStock()
        {

            //var bracnhes = _stockRepository.GetBranchesAsync();
            var branches = _stockRepository.GetBranches().Select(b => new SelectListItem { Value = b.BranchId.ToString(), Text = b.BranchName }).ToList();

            var model = new OrderModel
                {
                    BranchList = branches,
                };
                model.IsEdit = false;
                return PartialView("ManageStock", model);
            }

        [HttpPost]
        public async Task<IActionResult> ManageStock(int productId, int branchId, int quantity, string orderType)
        {
            if (quantity <= 0)
                return BadRequest("Quantity must be greater than zero");

            var stock = await _stockRepository.GetByProductAndBranchAsync(productId, branchId);

            if (stock == null)
            {
                stock = new Stock
                {
                    ProductId = productId,
                    BranchId = branchId,
                    Quantity = 0,
                    LastUpdated = DateTime.Now
                };

                await _stockRepository.AddAsync(stock);
            }

            if (orderType == "IN")
            {
                stock.Quantity += quantity;
            }
            else if (orderType == "OUT")
            {
                if (stock.Quantity < quantity)
                    return BadRequest("Insufficient stock");

                stock.Quantity -= quantity;
            }
            else
            {
                return BadRequest("Invalid order type");
            }

            stock.LastUpdated = DateTime.Now;
            await _stockRepository.UpdateAsync(stock);

            // Save order history
            var order = new Order
            {
                ProductId = productId,
                BranchId = branchId,
                Quantity = quantity,
                OrderType = orderType,
                CreatedAt = DateTime.Now
            };

            //await _orderRepository.AddAsync(order);

            return Ok();

        }

    }
}
