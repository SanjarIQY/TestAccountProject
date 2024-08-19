using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAccountProject.Models
{
    public enum Type 
    {
        Income,
        Expense
    }

    public enum IncomeCategory
    {
        RentaIncome,
        Salary,
        OtherIncome
    }

    public enum ExpenseCategory
    {
        Food,
        Transport,
        MobileNetwork,
        Internet,
        Entertainment,
        Other
    }

    [Table("transaction")]
    public class Transaction
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("type")]
        public Type Type { get; set; }
        [Column("expense_category")]
        public IncomeCategory ExpenseCategory { get; set; }
        [Column("income_category")]
        public ExpenseCategory IncomeCategory { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
    }
}
