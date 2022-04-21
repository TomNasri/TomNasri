using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MLB.Data
{
    public partial class Database
    {
        public void Initialize()
        {
            Persons = new List<PersonModel>();
            Persons.Add(new PersonModel("Perceval", "de Galles"));
            Persons.Add(new PersonModel("Bohort", "de Gaunes"));
            Persons.Add(new PersonModel("Léodagan", "de Carmélide"));
            Persons.Add(new PersonModel("Séli", ""));
            Persons.Add(new PersonModel("Guenièvre", ""));
            Persons.Add(new PersonModel("Karadoc", "de Vannes"));
            Persons.Add(new PersonModel("Kadoc", "de Vannes"));
            Persons.Add(new PersonModel("Lamorak", "de Galles"));
            Persons.Add(new PersonModel("Mevanwi", "de Vannes"));
            Persons.Add(new PersonModel("Alzagar", ""));
            Persons.Add(new PersonModel("Venec", ""));
            Persons.Add(new PersonModel("Quarto", ""));
            Persons.Add(new PersonModel("Horza", ""));
            Persons.Add(new PersonModel("Wulftan", ""));

            int i = 1;
            foreach (var person in Persons)
            {
                person.Id = i++;
                GenerateRandomExpenseNote(person);
            }
        }

        private void GenerateRandomExpenseNote(PersonModel person)
        {
            List<ExpenseNoteModel> list = new List<ExpenseNoteModel>();
            var r = new Random();

            var files = Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "files"));

            for (int i = 0; i < r.Next(2, 40); ++i)
                list.Add(new ExpenseNoteModel(person, r.Next(8, 45), DateTime.Now.Date.AddDays(-r.Next(1, 40)), GenerateName(), files[r.Next(0, files.Length - 1)]));

            person.ExpenseNotes = list.OrderBy(elt => elt.Date).ToList(); ;
        }

        private string GenerateName()
        {
            string[] names = new string[] {
                "Burger de Papa",
                "Burger King",
                "Kfc",
                "Campanile",
                "Novotel",
                "Airbnb",
                "Booking.com"
            };

            return names[new Random().Next(0, names.Length - 1)];
        }
    }
}
