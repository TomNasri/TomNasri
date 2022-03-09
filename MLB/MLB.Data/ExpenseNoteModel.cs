using System;

namespace MLB.Data
{
    public class ExpenseNoteModel : Model
    {
        public PersonModel Howner { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Institution { get; set; }

        public ExpenseNoteModel(PersonModel howner, decimal amount, DateTime date, string institution)
        {
            Howner = howner;
            Amount = amount;
            Date = date;
            Institution = institution;
        }
    }
}
