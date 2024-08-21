namespace TestAccountProject.Models
{ 
    public class FilterClass
    {
        public Type Type { get; set; }
        public ExpenseCategory ExpenseCategory { get; set; }
        public IncomeCategory IncomeCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime  EndDate { get; set; }
    }
}
