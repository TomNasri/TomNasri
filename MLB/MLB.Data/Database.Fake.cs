using System;
using System.Collections.Generic;
using System.Linq;

namespace MLB.Data
{
    public partial class Database
    {
        public void Initialize()
        {
            Persons = new List<PersonModel>();
            Persons.Add(new PersonModel("Joe", "Diggy"));
            Persons.Add(new PersonModel("Malcom", "Heist"));
            Persons.Add(new PersonModel("Luke", "Marchand"));
            Persons.Add(new PersonModel("James", "Stamom"));

            foreach (var person in Persons)
                GenerateRandomExpenseNote(person);
        }

        private void GenerateRandomExpenseNote(PersonModel person)
        {
            List<ExpenseNoteModel> list = new List<ExpenseNoteModel>();
            var r = new Random();

            for (int i = 0; i < r.Next(2, 40); ++i)
                list.Add(new ExpenseNoteModel(person, r.Next(8, 45), DateTime.Now.Date.AddDays(-r.Next(1, 40)), GenerateName()));

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
