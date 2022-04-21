using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MLB.Service;
using System;
using System.IO;

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

        [HttpPost]
        [Route("/Person/{id}")]
        public IActionResult Detail(int id, string institution, decimal amount, DateTime date, IFormFile file)
        {
            var person = PersonService.Get(id);

            if (file != null)
            {
                string path = DownloadFile(file);

                PersonService.AddExpenseNote(person, institution, amount, date, path);
            }

            return Redirect($"/Person/{id}");
        }

        private string DownloadFile(IFormFile file)
        {
            int index = file.FileName.LastIndexOf('.');
            if (index <= 0)
                throw new Exception("Invalid file");

            string extension = file.FileName.Substring(index + 1);
            string name = $"tmp_{Guid.NewGuid()}.{extension}";
            string tempDirectory = Path.Combine(Environment.CurrentDirectory, "temp");

            if (!Directory.Exists(tempDirectory))
                Directory.CreateDirectory(tempDirectory);

            string tempPath = Path.Combine(tempDirectory, name);

            byte[] buffer = new byte[4096];
            int n;

            using (var reader = file.OpenReadStream())
            {
                using (var writer = System.IO.File.Create(tempPath))
                {
                    while ((n = reader.Read(buffer, 0, buffer.Length)) > 0)
                        writer.Write(buffer, 0, n);
                }
            }

            string filesDirectory = Path.Combine(Environment.CurrentDirectory, "files");

            if (!Directory.Exists(filesDirectory))
                Directory.CreateDirectory(filesDirectory);

            string destination = Path.Combine(filesDirectory, name);

            System.IO.File.Move(tempPath, destination);

            return destination;
        }
    }
}