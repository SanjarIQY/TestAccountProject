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

    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Type Type { get; set; }
        public IncomeCategory ExpenseCategory { get; set; }
        public ExpenseCategory IncomeCategory { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
    }
}
