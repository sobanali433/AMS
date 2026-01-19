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
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {

            //ViewData["ReturnUrl"] = returnUrl;
            return View(new UserMasterModel());

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
            var user = await _accountRepository.GetByUsernameAsync(model.Username);


            if (user == null || !user.IsActive)
                return View("Login");

            var hasher = new PasswordHasher<UserMaster>();
            var result = hasher.VerifyHashedPassword(user, user.UserPassword, model.Userpassword
            );
            if (result != PasswordVerificationResult.Success)
                return View("Login");

            var claims = new List<Claim>
        {
             new Claim(ClaimTypes.Name, user.Username),
             new Claim("FirstName", user.FirstName ?? "Guest"),
             new Claim("LastName", user.LastName ?? "Guest"),
             new Claim("RoleName", user.Role?.RoleName ?? "Guest"),
             new Claim(ClaimTypes.NameIdentifier, user.UserMasterId.ToString()),
             new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "Guest")
                };  


            var identity = new ClaimsIdentity(claims, "AMSCookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("AMSCookies", principal);

            return RedirectToAction("Index", "User");
        }


        //[HttpPost]
        //public async Task<IActionResult> Login(UserMasterModel model)
        //{
        //    try
        //    {
        //         var user = await _accountRepository.GetByUsernameAndPasswordAsync(model.Username, model.Userpassword);
        //        //var userr = _accountRepository.GetByUsernameAsync(model.Username, model.RoleName);

        //        if (user == null)
        //        {
        //            ModelState.AddModelError("", "Invalid credentials or account inactive.");
        //            return View(model);
        //        }

        //        var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.UserMasterId.ToString()),
        //    new Claim(ClaimTypes.Name, user.Username ?? ""),
        //    new Claim("FirstName", user.FirstName ?? ""),
        //    new Claim("LastName", user.LastName ?? ""),
        //    new Claim("RoleName", user?.Role?.RoleName ?? "User"),
        //    new Claim(ClaimTypes.Role, user?.Role?.RoleName ?? "User")
        //};

        //        var claimsIdentity = new ClaimsIdentity(claims, "AMSCookies");
        //        var authProperties = new AuthenticationProperties
        //        {
        //            AllowRefresh = true,
        //            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        //        };

        //        await HttpContext.SignInAsync(
        //            scheme: "AMSCookies",
        //            principal: new ClaimsPrincipal(claimsIdentity),
        //            properties: authProperties);

        //        int roleId = user.RoleId;

        //        if (roleId == 2)
        //            return RedirectToAction("Index", "User");
        //        else
        //            return RedirectToAction("Index", "User");
        //    }
        //    catch (Exception)
        //    {
        //        ModelState.AddModelError("", "Please insert correct credentials.");
        //        return View(model);
        //    }
        //}

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("AMSCookies");
            return RedirectToAction("Login");
        }



        //AuthResultModel res = new AuthResultModel();
        //loginModel.SessionId = _sessionManager.GetSessionId();
        //res = _accountRepository.Authentication(loginModel, _sessionManager.GetSessionId(), _sessionManager.GetIP());

    }



}

