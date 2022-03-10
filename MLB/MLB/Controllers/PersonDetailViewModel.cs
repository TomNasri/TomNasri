using Microsoft.AspNetCore.Mvc;
using MLB.Data;

namespace MLB.Controllers
{
    public class PersonDetailViewModel : HomeViewModel

    {
        public PersonModel Person { get; set; }

        public PersonDetailViewModel(Controller controller) : base(controller)
        {

        }
    }
}
