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
    [Authorize]
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
            var roles = _userRepository.GetRoles().Select(r => new SelectListItem{Value = r.RoleId.ToString(),Text = r.RoleName}  ).ToList();
            var branches = _userRepository.GetBranches().Select(b => new SelectListItem{Value = b.BranchId.ToString(),Text = b.BranchName}).ToList();
            if (id == null)
            {
                var model = new UserMasterModel
                {
                    RoleList = roles,
                    BranchList = branches,
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
                    BranchId = user.BranchId,
                    IsActive = user.IsActive
                };

                return PartialView("_Details", model);
            }
        }
        [HttpPost]
        public JsonResult GetList()
        {
            //var data = _userRepository.GetAllUsersWithoutSuperAdmin();

            var user = _userRepository.GetList();

            var result = new
            {
                draw = Request.Form["draw"].FirstOrDefault(),
                recordsTotal = user.Count,
                recordsFiltered = user.Count,
                data = user
            };
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserMasterModel model)
        {
            try
            {
                if (model.UserMasterId == 0 || model.UserMasterId == null)
                {
                    if (string.IsNullOrWhiteSpace(model.Userpassword)) 
                    {
                        ModelState.AddModelError("", "Password required");
                        return View(model);
                    }
                    if (model.IsActive == false) 
                    {
                        return Json(new { isSuccess = false, message = "User is inActive" });

                    }
                    var hasher = new PasswordHasher<UserMaster>();
                    string masterpass = "AQAAAAIAAYagAAAAEKbmHHcuqZ/OW6UmxvfWFv8mYvJnJTnUtrSMDGJfRXtgopBgbU5eoP2/4S4UsDBhXA==";

                    var user = new UserMaster
                    {
                        UserMasterId = model.UserMasterId,
                        Username = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        RoleId = model.RoleId,
                        IsActive = model.IsActive,
                        DateOfBirth = model.DateOfBirth,
                        Gender   = model.Gender,
                        Ip = model.Ip,
                        ContactNumber = model.ContactNumber,
                        CreatedBy = model.CreatedBy,
                        UserMasterPassword = masterpass, 
                        BranchId = model.BranchId,
                    };

                    user.UserPassword = hasher.HashPassword(user, model.Userpassword);
                    var result = await _userRepository.AddUserAsync(user);
                    return Json(new { isSuccess = result, message = result ? "User added successfully" : "Failed to add user" });
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
