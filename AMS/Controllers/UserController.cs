using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AMS.Controllers
{
    [Authorize (Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;

        public UserController(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            //var users = await _userRepository.GetAllUsersWithoutSuperAdmin();

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
                var model = new UserMasterModel
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
                var model = new UserMasterModel
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
        public async Task<IActionResult> Save(UserMasterModel model)
        {
            Console.WriteLine(model.FirstName);
            Console.WriteLine(model.LastName);
            Console.WriteLine(model.Userpassword);
            Console.WriteLine(model.UserMasterId);
            Console.WriteLine(model.RoleId);
            Console.WriteLine(model.Ip);
            Console.WriteLine(model.DateOfBirth);
            Console.WriteLine(model.Gender);
            Console.WriteLine(model.RoleName);
            Console.WriteLine(model.ContactNumber);
            Console.WriteLine(model.IsActive);
            Console.WriteLine(model.CreatedDate);
            Console.WriteLine(model.IsFirstTimeLogin);
            Console.WriteLine(model.Userpassword);

            //if (!ModelState.IsValid) 
            //{
            //    return View(model);
            //}
            try
            {
                if (model.UserMasterId == 0 || model.UserMasterId == null)
                {
                    if (string.IsNullOrWhiteSpace(model.Userpassword)) 
                    {
                        ModelState.AddModelError("", "Password required");
                        return View(model);
                    }
                    var hasher = new PasswordHasher<UserMaster>();
                    var user = new UserMaster
                    {
                        UserMasterId = model.UserMasterId,
                        Username = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        RoleId = model.RoleId,
                        IsActive = model.IsActive

                    };
                    user.UserPassword = hasher.HashPassword(user, model.Userpassword);
                    bool result =  Convert.ToBoolean(_userRepository.AddUserAsync(user));

                    return Json(new { isSuccess = result, message = result ? "User added successfully" : "Failed to add user" });

                    //var result = await _userRepository.AddUserAsync(model);
                }
                else 
                {
                    var existingUser = await _userRepository.GetByIdAsync(model.UserMasterId);
                    if (existingUser == null)
                        return Json(new { isSuccess = false, message = "User not found" });


                    existingUser.Username = model.Username;
                    existingUser.UserPassword = model.Userpassword;
                    existingUser.FirstName = model.FirstName;
                    existingUser.LastName = model.LastName;
                    existingUser.ContactNumber = model.ContactNumber;
                    existingUser.RoleId = model.RoleId;
                    existingUser.UpdatedOn = DateTime.Now;

                    if (!string.IsNullOrWhiteSpace(model.Userpassword))
                    {
                        var hasher = new PasswordHasher<UserMaster>();
                        model.Userpassword = hasher.HashPassword(existingUser,model.Userpassword);
                    }

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
