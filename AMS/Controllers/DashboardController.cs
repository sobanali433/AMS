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
        public DashboardController(IDashboardRepository dashboardRepository,IAccountRepository accountRepository)
        {
            _dashboardRepository = dashboardRepository;
            _accountRepository = accountRepository;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]

        public async Task<IActionResult> AdminDashboard()
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
                RoleName = user.Role.RoleName
            };

            return View(model);
        }


        public async Task<IActionResult> UserDashboard()
        {
            var username = User.Identity?.Name;

            // await the task to get the actual user object
            var user = await _dashboardRepository.HeaderlayoutAsync(username);

            if (user == null)
            {
                return RedirectToAction("Logout", "Account");
            }

            var model = new UserMasterModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleName = user.Role.RoleName,
            };

            return View(model);


        }
}
}
