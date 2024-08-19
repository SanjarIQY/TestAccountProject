using Microsoft.EntityFrameworkCore;
using TestAccountProject.Models;

namespace TestAccountProject.Models
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AppDbContext(IConfiguration conf)
        {
            this.configuration = conf;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(configuration.GetConnectionString("EFDemo"));
        }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
