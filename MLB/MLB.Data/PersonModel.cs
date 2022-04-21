using System.Collections.Generic;

namespace MLB.Data
{
    public class PersonModel : Model
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public List<ExpenseNoteModel> ExpenseNotes { get; set; }

        public PersonModel(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }
}
