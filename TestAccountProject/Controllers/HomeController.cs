using Microsoft.AspNetCore.Mvc;
using TestAccountProject.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TestAccountProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Retrieve all transactions from the database
            var transactions = await _context.Transactions.ToListAsync();
            ViewBag.Transactions = transactions;
            return View();
        }

        // POST: /Home/Index
        [HttpPost]
        public async Task<IActionResult> Index(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                // Add the new transaction to the database
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                // Redirect to the GET Index method to display the updated list
                return RedirectToAction(nameof(Index));
            }

            // If there are validation errors, return the view with current transactions
            var transactions = await _context.Transactions.ToListAsync();
            ViewBag.Transactions = transactions;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public string Privacy()
        {
            return "The page is not completed yet";
        }
    }
}