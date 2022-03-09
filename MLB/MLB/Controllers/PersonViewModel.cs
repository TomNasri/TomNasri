using Microsoft.AspNetCore.Mvc;
using MLB.Data;
using System.Collections.Generic;

namespace MLB.Controllers
{
    public class PersonViewModel : HomeViewModel
    {
        public List<PersonModel> Persons { get; set; }

        public PersonViewModel(Controller controller) : base(controller)
        {
            Persons = Database.Self.Persons;
        }
    }
}
