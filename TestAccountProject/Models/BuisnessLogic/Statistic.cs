namespace TestAccountProject.Models.BuisnessLogic
{
    public class Statistic
    {
        public IEnumerable<Transaction>? Transactions { get; set; }
        public decimal? Summ { get; set; }
    }
}
