using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Jogging.Web.Models;
using Jogging.Web.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Jogging.Web.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        public ApplicationUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IMapper _mapper;

        public HomeController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            var user = UserManager.FindByEmail(email);
            if (user != null && UserManager.CheckPassword(user, password))
            {
                SignInManager.SignIn(user, true, false);
                return Json(_mapper.Map<UserViewModel>(user));
            }

            return null;
        }

        [HttpPost]
        public void Logout(string id)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        [HttpPost]
        public JsonResult Register(string name, string email, string password)
        {
            var user = new ApplicationUser { UserName = name, Email = email };
            var result = UserManager.Create(user, password);
            if (result.Succeeded)
            {
                SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                return Json(UserManager.FindByName(email));
            }

            return null;
        }
    }
}
