namespace TestAccountProject.Models
{
    public class Statistic
    {
        public IEnumerable<Transaction> Transactions { get; set; }
        public decimal? Income {  get; set; }
        public decimal? Outcome { get; set; }
    }
}
