namespace TestAccountProject.Models
{
    public enum Months
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    public class FilterClass
    {
        public Type Type { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public Months Month { get; set; }
    }
}
