using Microsoft.AspNetCore.Mvc;
using MLB.Service;

namespace MLB.Controllers
{
    public class PersonController : Controller
    {
        public PersonService PersonService { get; }

        public PersonController(PersonService personService)
        {
            PersonService = personService;
        }


        public IActionResult Index()
        {
            return View(new PersonViewModel(this));
        }

        [HttpPost]
        public IActionResult Index(string firstname, string lastname)
        {
            PersonService.Create(firstname, lastname);

            return Index();
        }

        [Route("/Person/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            PersonService.Delete(id);
            return Redirect("/Person");
        }

        [Route("/Person/{id}")]
        public IActionResult Detail(int id)
        {
            var vm = new PersonDetailViewModel(this);
            vm.Person = PersonService.Get(id); ;

            return View(vm);
        }
    }
}