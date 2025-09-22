using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        public IActionResult _Details(int? id)
        {
            var roles = _userRepository.GetRoles()
           .Select(r => new SelectListItem { Value = r.RoleId.ToString(), Text = r.RoleName }).ToList();
            if (id == null)
            {
                var model = new UserViewModel
                {
                    RoleList = roles
                };
                return PartialView("_Details", model);
            }
            else {
                var user = _userRepository.GetById(id.Value);
                if (user == null) { 
                return NotFound();  
                }
                var model = new UserViewModel
                {
                    UserMasterId = user.UserMasterId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ContactNumber = user.ContactNumber,
                    RoleId = user.RoleId,
                    RoleList = roles
                
                };

                return PartialView("_Details", model);
            }



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
