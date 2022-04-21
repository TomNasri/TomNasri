using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MLB.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View(new LoginViewModel(this));
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            LoginViewModel vm = new LoginViewModel(this);
            if (username == "test")
            {
                vm.IsAuthenticated = true;
                vm.Login = username;
                //[Security] Injection
                HttpContext.Session.SetString("login", username);

            }
            else
                vm.Error = "Bad Username or password (try with test/test)";
            return View(vm);
        }

        public IActionResult Disconnect()
        {
            HttpContext.Session.Remove("login");

            return Redirect("/login");
        }
    }
}
