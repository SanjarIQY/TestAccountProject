using Microsoft.EntityFrameworkCore;
using TestAccountProject.Models;

namespace TestAccountProject.Models
{
    public class Context : DbContext
    {
        protected readonly IConfiguration conf;
        public Context(IConfiguration _conf)
        {
            conf = _conf;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql();
        }

        DbSet<Transaction> Transactions { get; set; }
    }
}
