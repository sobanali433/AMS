using AMS.Data;
using AMS.Models;
using AMS.Repository;
using AMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public const string Temp_Success = "Success";
        public const string Temp_Error = "Error";
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {

            //ViewData["ReturnUrl"] = returnUrl;
            //return View(new UserMasterModel());
            return View();

            //CaptchaResult captcha = Captcha.Generate(CaptchaType.Simple);
            //_sessionManager.CaptchaCode = captcha.CatpchaCode;
            //LoginModel model = new LoginModel
            //{
            //    CaptchaImage = captcha.CaptchaBase64
            //};

            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserMasterModel model)
        {
            try
            {
                var user = await _accountRepository.GetByUsernameAsync(model.Username);


                if (user == null || !user.IsActive)
                    return View("Login");

                var hasher = new PasswordHasher<UserMaster>();
                var result = hasher.VerifyHashedPassword(user, user.UserPassword, model.Userpassword);

                if (result != PasswordVerificationResult.Success)
                    return View("Login");

                var claims = new List<Claim>
        {
             new Claim(ClaimTypes.Name, user.Username),
             new Claim("FirstName", user.FirstName ?? "Guest"),
             new Claim("LastName", user.LastName ?? "Guest"),
             new Claim("RoleName", user.Roles?.RoleName ?? "Guest"),
             new Claim(ClaimTypes.NameIdentifier, user.UserMasterId.ToString()),
             new Claim(ClaimTypes.Role, user.Roles?.RoleName ?? "Guest")
                };


                var identity = new ClaimsIdentity(claims, "AMSCookies");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("AMSCookies", principal);


                if (user.Roles?.RoleName == "SuperAdmin")
                {
                    return RedirectToAction("AdminDashboard", "Dashboard");

                }
                return RedirectToAction("UserDashboard", "Dashboard");
                //return RedirectToAction("Index", "User");

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Account");
                throw;

            }
        }




        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AMSCookies");
            return RedirectToAction("Login");
        }


    }



}

