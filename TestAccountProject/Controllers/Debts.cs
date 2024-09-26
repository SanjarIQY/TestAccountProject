using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestAccountProject.Models;

namespace TestAccountProject.Controllers
{
    public class Debts : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly AppDbContext db;

        public Debts(UserManager<IdentityUser> userManager, AppDbContext dbContext)
        {
            this.userManager = userManager;
            this.db = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
