using AMS.Data;
using AMS.Models;
using AMS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AMS.Controllers
{
    public class BranchMasterController : Controller
    {
        private readonly IBranchRepository _branchRepository;

        public BranchMasterController(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetList()
        {
            var user = _branchRepository.GetAllAsync();

            var data = user.Select(s => new
            {
                s.BranchId,
                BranchName = s.BranchName,
                Location = s.Location,
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt.ToString("MM/dd/yyyy hh:mm tt")
            }).ToList();

            return Json(new
            {
                data = data
            });
        }
        [HttpGet]
        public IActionResult _Details(int? id)
        {

            if (id == null)
            {
                var model = new BranchModel
                {

                };
                model.IsEdit = false;
                return PartialView("_Details", model);
            }
            else
            {
                var user = _branchRepository.GetById(id.Value);
                if (user == null)
                {
                    return NotFound();
                }
                var model = new UserMasterModel
                {
                    //UserMasterId = user.UserMasterId,
                    //Username = user.Username,
                    //FirstName = user.FirstName,
                    //LastName = user.LastName,
                    //ContactNumber = user.ContactNumber,
                    //RoleId = user.RoleId,
                    //RoleName = user.Roles.RoleName,
                    //BranchId = user.BranchId,
                    //IsActive = user.IsActive,
                    //CreatedOn = user.CreatedOn,
                    //BranchName = user.BranchMasters.BranchName,
                    //Gender = user.Gender,
                    //DateOfBirth = user.DateOfBirth,
                    //RoleList = roles,
                    //BranchList = branches,
                    //GenderList = genders,
                    //IsEdit = true
                };
                return PartialView("_Details", model);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Save(BranchModel model)
        {
            try
            {
                if (model.BranchId == 0 || model.BranchId == null)
                {
                    if (model.IsActive == false)
                    {
                        return Json(new { isSuccess = false, message = "User is inActive" });

                    }

                    var user = new BranchMaster
                    {
                        BranchName = model.BranchName,
                        Location = model.Location,
                        BranchId = model.BranchId,
                        IsActive = model.IsActive,
                        CreatedAt = DateTime.UtcNow

                    };
                    var result = await _branchRepository.AddBranchAsync(user);


                    return Json(new { isSuccess = result, message = result ? "Branch added successfully" : "Failed to add branch" });
                }
                else
                {
                    var existingUser =  _branchRepository.GetById(model.BranchId);
                    if (existingUser == null)
                        return Json(new { isSuccess = false, message = "User not found" });


                    existingUser.BranchId = model.BranchId;
                    existingUser.BranchName= model.BranchName;
                    existingUser.Location = model.Location;
                    existingUser.CreatedAt = DateTime.UtcNow;
                    existingUser.IsActive = model.IsActive;


                    var result = await _branchRepository.UpdateUserAsync(existingUser);

                    return Json(new { isSuccess = result, message = result ? "Branch updated successfully" : "Failed to update Branch" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = ex.Message });
            }
        }

        [HttpPost]

        public IActionResult Delete(int id)
        {
            if (id == null)
                return Json(new { success = false, message = "Invalid ID" });

            var result = _branchRepository.Delete(id);

            return Json(new { isSuccess = result.isSuccess, message = result.message });

        }

    }
}
