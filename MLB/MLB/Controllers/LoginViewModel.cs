using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MLB.Controllers
{
    public class LoginViewModel : HomeViewModel
    {
        public string Error { get; set; }
        public bool IsAuthenticated { get; set; }

        public LoginViewModel(Controller controller) : base(controller)
        {

        }
    }
}
