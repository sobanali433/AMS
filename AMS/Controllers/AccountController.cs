using AMS.Data;
using AMS.Models;
using AMS.Repository;
using AMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var user = await _accountRepository.GetByUsernameAndPasswordAsync(model.Username, model.Userpassword);

            if (ModelState.IsValid) return View(model);

            var user1 = _accountRepository.GetByUsernameAsync(model.Username, model.RoleName);
            if (user == null || model.IsActive)
            {
                ModelState.AddModelError("", "Invalid credentials or account inactive.");
                return View(model);
            }
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserMasterId.ToString()),
            new Claim(ClaimTypes.Name, user.Username ?? ""),
                //new Claim("FullName", user.FullName ?? ""),
                new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName ?? ""),
                new Claim("RoleName", user?.Role?.RoleName ?? "User"),
                new Claim(ClaimTypes.Role, user?.Role?.RoleName ?? "User") 
                //new Claim(ClaimTypes.Role, user.RoleName),

            };
            var claimsIdentity = new ClaimsIdentity(claims, "AMSCookies");
            var authProperties = new AuthenticationProperties
            {
                //IsPersistent = model.RememberMe,
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) // set as you like
            };

            HttpContext.SignInAsync(
           scheme: "AMSCookies",
           principal: new ClaimsPrincipal(claimsIdentity),
           properties: authProperties);

            int roleId = user.RoleId;

            if (roleId == 2)
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
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

