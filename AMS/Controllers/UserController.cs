using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AMS.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public UserController(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            //var username = User.Identity?.Name;
            //var user = await _userRepository.HeaderlayoutAsync(username);
            //if (user == null)
            //{
            //    return RedirectToAction("Logout", "Account");
            //}

            //var model = new UserMasterModel
            //{
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    RoleName = user.Role.RoleName
            //};


            return View();
        }

        public IActionResult _Details(string id)
        {

            return View();
        }
        public JsonResult GetList()
        {
            var data = _userRepository.GetList();
            var result = new
            {
                draw = Request.Form["draw"].FirstOrDefault(),
                recordsTotal = data.Count,   
                recordsFiltered = data.Count,
                data = data
            };

            return Json(result);
        }
    }
}
