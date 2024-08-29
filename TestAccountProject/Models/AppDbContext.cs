using Microsoft.EntityFrameworkCore;
using TestAccountProject.Models;

namespace TestAccountProject.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
