using FootballManager.Contracts;
using FootballManager.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;

namespace FootballManager.Controllers
{
    public class UsersController : Controller
    {
        private IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        public HttpResponse Login()
            => View();

        [HttpPost]
        public HttpResponse Login(LogInUserFormModel model)
        {
            if (!userService.UserIsValid(model))
            {
                return View();
            }

            SignIn(GetUserId(model));
            return Redirect("/Players/All");
        }
        public HttpResponse Register()
            => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            bool isRegistered = userService.RegisterUser(model);
            if (!isRegistered)
                return View();

            return Redirect("/Users/Login");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            SignOut();
            return Redirect("/");
        }

        private string GetUserId(LogInUserFormModel model)
            => userService.GetUserId(model);
    }
}
