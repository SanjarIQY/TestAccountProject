using Microsoft.EntityFrameworkCore;
using TestAccountProject.Models.BuisnessLogic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace TestAccountProject.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Transaction> Transaction { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
