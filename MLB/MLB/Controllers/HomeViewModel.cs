using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MLB.Controllers
{
    public class HomeViewModel
    {
        public string Login { get; set; }
        public bool IsAuthenticated { get; set; }

        public HomeViewModel(Controller controller)
        {
            string login = controller.HttpContext.Session.GetString("login");

            if (login != null)
            {
                IsAuthenticated = true;
                Login = login;
            }
        }

    }
}
