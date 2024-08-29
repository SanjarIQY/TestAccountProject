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
            return View(await db.Transactions.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            transaction.Date = TimeFixer(transaction.Date);

            await db.SaveChangesAsync();
            return View(await db.Transactions.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            return View(new StatsViewModel()
            {
                Statistic = new Statistic() { Transactions = null },
            });
        }

        [HttpPost]
        public async Task<IActionResult> Stats(FilterClass req)
        {
            req.StartDate = TimeFixer(req.StartDate);
            req.EndDate = TimeFixer(req.EndDate);

            List<Transaction>? stats = null;

            if (db.Transactions.Count() != 0)
            {
                if ((int)req.IncomeCategory == 4 || (int)req.ExpenseCategory == 7)
                {
                    stats = await db.Transactions.AsQueryable().Where(d => d.Date >= req.StartDate && d.Date <= req.EndDate && d.Type == req.Type).ToListAsync();
                }
                else
                {
                    stats = await db.Transactions.AsQueryable().Where(d => d.Date >= req.StartDate && d.Date <= req.EndDate && d.Type == req.Type && ((int)req.Type == 1 ? req.IncomeCategory == d.IncomeCategory : req.ExpenseCategory == d.ExpenseCategory)).ToListAsync();
                }
            }

            return View(new StatsViewModel()
            {
                Statistic = new Statistic() { Summ = Summ(stats), Transactions = stats },
                FilterClass = req,
            });
        }

        public decimal Summ(List<Transaction> transactions)
        {
            if (transactions is null)
            {
                return 0;
            }
            decimal summ = 0;
            foreach (var item in transactions)
            {
                summ += item.Amount;
            }

            return summ;
        }

        public DateTime TimeFixer(DateTime date)
        {
            if (date.Date.Kind == DateTimeKind.Unspecified)
            {
                date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            }
            else if (date.Kind == DateTimeKind.Local)
            {
                date = date.ToUniversalTime();
            }

            return date;
        }

    }
}