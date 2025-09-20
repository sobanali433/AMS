//using AMS.Models;
//using AMS.Services;
//using Microsoft.AspNetCore.Mvc;

//namespace AMS.Controllers
//{
//    public class BaseController : Controller
//    {
//        public ISessionManager _sessionManager;


//        public BaseController(ISessionManager sessionManager)
//        {
//            _sessionManager = sessionManager;
//        }

//        [NonAction]
//        protected SessionProviderModel GetSessionProviderParameters()
//        {
//            SessionProviderModel sessionProviderModel = new SessionProviderModel
//            {
//                UserId = _sessionManager.UserId,
//                Username = _sessionManager.Username,
//                Ip = _sessionManager.GetIP(),
//                FirstName = _sessionManager.FirstName,
//                LastName = _sessionManager.LastName,
//                RoleId = _sessionManager.RoleId,
//                RoleName = _sessionManager.RoleName,
//            };
//            return sessionProviderModel;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
