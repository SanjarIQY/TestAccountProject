using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAccountProject.Models
{
    public enum Type 
    {
        Income = 1,
        Expense = 2
    }

    public enum IncomeCategory
    {
        RentaIncome = 1,
        Salary = 2,
        OtherIncome = 3
    }

    public enum ExpenseCategory
    {
        Food = 1,
        Transport = 2,
        MobileNetwork = 3,
        Internet = 4,
        Entertainment = 5,
        Other = 6
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
        public IncomeCategory? IncomeCategory { get; set; }
        [Column("income_category")]
        public ExpenseCategory? ExpenseCategory { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("comment")]
        public string Comment { get; set; }
    }
}
