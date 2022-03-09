using MLB.Data;
using System.Collections.Generic;

namespace MLB.Service
{
    public class PersonService
    {
        public List<PersonModel> GetPersons()
        {
            return Database.Self.Persons;
        }
    }
}
