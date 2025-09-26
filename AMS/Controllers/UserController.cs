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
            else
            {
                var user = _userRepository.GetById(id.Value);
                if (user == null)
                {
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

        [HttpPost]
        public async Task<IActionResult> Save(UserMaster model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Json(new { isSuccess = false, message = "Invalid data" });
            //}
            try
            {
                if (model.UserMasterId == 0)
                {
                    var result = await _userRepository.AddUserAsync(model);
                    return Json(new { isSuccess = result, message = result ? "User added successfully" : "Failed to add user" });
                }
                else 
                {
                    var existingUser = await _userRepository.GetByIdAsync(model.UserMasterId);
                    if (existingUser == null)
                        return Json(new { isSuccess = false, message = "User not found" });

                    existingUser.Username = model.Username;
                    existingUser.UserPassword = model.UserPassword;
                    existingUser.FirstName = model.FirstName;
                    existingUser.LastName = model.LastName;
                    existingUser.ContactNumber = model.ContactNumber;
                    existingUser.RoleId = model.RoleId;
                    existingUser.UpdatedOn = DateTime.Now;

                    var result = await _userRepository.UpdateUserAsync(existingUser);
                    return Json(new { isSuccess = result, message = result ? "User updated successfully" : "Failed to update user" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = ex.Message });
            }
        }

    }

}
