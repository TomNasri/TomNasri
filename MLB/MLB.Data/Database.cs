using System.Collections.Generic;

namespace MLB.Data
{
    public partial class Database
    {
        private static Database _database;

        public List<PersonModel> Persons { get; set; }

        public static Database Self
        {
            get
            {
                if (_database == null)
                {
                    _database = new Database();
                    _database.Initialize();
                }

                return _database;
            }
        }

        public Database()
        {

        }
    }
}
