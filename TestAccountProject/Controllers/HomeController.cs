using Microsoft.AspNetCore.Mvc;
using TestAccountProject.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestAccountProject.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db;
        public HomeController(AppDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await db.Transaction.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Transaction transaction)
        {
            db.Transaction.Add(transaction);
            if (transaction.Date.Kind == DateTimeKind.Unspecified)
            {
                transaction.Date = DateTime.SpecifyKind(transaction.Date, DateTimeKind.Utc);
            }
            else if (transaction.Date.Kind == DateTimeKind.Local)
            {
                transaction.Date = transaction.Date.ToUniversalTime();
            }

            await db.SaveChangesAsync();
            return View(await db.Transaction.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            return View(new StatsViewModel()
            {
                Statistic = new Statistic() { Income = 10, Outcome = 20, Transactions = db.Transaction.ToList() },
            });
        }

        [HttpPost]
        public async Task<IActionResult> Stats(FilterClass req)
        {
            ///statsitika
            return View(new StatsViewModel()
            {
                Statistic = new Statistic() { Income = 10, Outcome = 20, Transactions = db.Transaction.ToList() },
                FilterClass = req,
            });
        }

    }
}