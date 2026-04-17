using AMS.Data;
using AMS.Migrations;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace AMS.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IOrderRepository _orderRepository;
        public DashboardController(IOrderRepository orderRepository , IDashboardRepository dashboardRepository,IStockRepository stockRepository , IAccountRepository accountRepository, IUserRepository userRepository, IProductRepository productRepository  )
        {
            _dashboardRepository = dashboardRepository;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _orderRepository = orderRepository;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> AdminDashboard()
        {
            var totalUsers = _userRepository.GetTotalUsers();
            var totalProducts = _productRepository.GetTotalProducts();
            var stockIn = _orderRepository.GetStocksIn();
            var stockOut = _orderRepository.GetStocksOut();
            ViewBag.totalUsers = totalUsers;
            ViewBag.totalProducts = totalProducts;
            ViewBag.stockIn = stockIn;
            ViewBag.stockOut = stockOut;
            var username = User.Identity?.Name;
            var user = await _dashboardRepository.HeaderlayoutAsync(username);
            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            var model = new UserMasterModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleName = user.Roles.RoleName
            };
            return View(model);
        }


        public async Task<IActionResult> UserDashboard()
        {
            var username = User.Identity?.Name;

            var user = await _dashboardRepository.HeaderlayoutAsync(username);

            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            var model = new UserMasterModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleName = user.Roles.RoleName,
            };
            return View(model);


        }
}
}
