using Microsoft.AspNetCore.Mvc;
using TestAccountProject.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestAccountProject.Models.BuisnessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Immutable;


namespace TestAccountProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signinManager;
        public HomeController(AppDbContext context, UserManager<IdentityUser> UM, SignInManager<IdentityUser> SM)
        {
            db = context;
            _userManager = UM;
            _signinManager = SM;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var transactions = await db.Transaction.Where(x => x.User.Id == user.Id).ToListAsync();
            return View(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Transaction transaction)
        {
            var user = await _userManager.GetUserAsync(User);
            transaction.User = user;
            transaction.UserId = user.Id;
            db.Transaction.Add(transaction);
            transaction.Date = TimeFixer(transaction.Date);

            await db.SaveChangesAsync();
            return View(await db.Transaction.ToListAsync());
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
            var user = await _userManager.GetUserAsync(User);
            req.StartDate = TimeFixer(req.StartDate);
            req.EndDate = TimeFixer(req.EndDate);

            List<Transaction>? stats = null;

            if (db.Transaction.Count() != 0)
            {
                if ((int)req.IncomeCategory == 4 || (int)req.ExpenseCategory == 7)
                {
                    stats = await db.Transaction.AsQueryable()
                        .Where(d => d.Date >= req.StartDate && d.Date <= req.EndDate && d.Type == req.Type && d.UserId == user.Id)
                        .ToListAsync();
                }
                else
                {
                    stats = await db.Transaction.AsQueryable()
                        .Where(d => d.Date >= req.StartDate && d.Date <= req.EndDate && d.Type == req.Type && ((int)req.Type == 1 ? req.IncomeCategory == d.IncomeCategory : req.ExpenseCategory == d.ExpenseCategory) && d.UserId == user.Id)
                        .ToListAsync();
                }
            }

            return View(new StatsViewModel()
            {
                Statistic = new Statistic() { Summ = Summ(stats), Transactions = stats },
                FilterClass = req,
            });
        }

        public decimal Summ(List<Transaction>? transactions)
        {
            if (transactions == null)
                return 0;

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