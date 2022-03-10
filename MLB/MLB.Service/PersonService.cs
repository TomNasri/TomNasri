using MLB.Data;
using System.Collections.Generic;
using System.Linq;

namespace MLB.Service
{
    public class PersonService
    {
        public List<PersonModel> GetPersons()
        {
            return Database.Self.Persons;
        }

        public void Create(string firstname, string lastname)
        {
            var p = new PersonModel(firstname, lastname);

            p.Id = Database.Self.Persons.Max(elt => elt.Id) + 1;
            p.ExpenseNotes = new List<ExpenseNoteModel>();

            Database.Self.Persons.Add(p);
        }

        public void Delete(int id)
        {
            var p = Database.Self.Persons.SingleOrDefault(elt => elt.Id == id);

            if (p != null)
                Database.Self.Persons.Remove(p);
        }

        public PersonModel Get(int id)
        {
            return Database.Self.Persons.SingleOrDefault(elt => elt.Id == id);
        }
    }
}
