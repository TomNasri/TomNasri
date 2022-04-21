using System;
using System.Text;

namespace MLB.Data
{
    public class ExpenseNoteModel : Model
    {
        public PersonModel Howner { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Institution { get; set; }
        public string ProofPath { get; set; }
        public string ProofWebPath { get; set; }

        public ExpenseNoteModel(PersonModel howner, decimal amount, DateTime date, string institution, string proofPath)
        {
            Howner = howner;
            Amount = amount;
            Date = date;
            Institution = institution;
            ProofPath = proofPath;
            var sb = new StringBuilder(proofPath);
            sb.Replace(Environment.CurrentDirectory, string.Empty);
            sb.Replace("\\", "/");
            sb.Replace("/files/", string.Empty);
            ProofWebPath = sb.ToString();
        }
    }
}
