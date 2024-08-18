using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestAccountProject.Models;

namespace TestAccountProject.Controllers
{
    public class HomeController : Controller
    {
        private static List<Transaction> _transactions = new List<Transaction>();

        [HttpGet]
        public IActionResult Index()
        {
            // Pass the list of transactions to the view
            ViewBag.Transactions = _transactions;
            return View();
        }

        [HttpPost]
        public IActionResult Index(Transaction transaction)
        {
            // Add the new transaction to the list
            _transactions.Add(transaction);

            // Pass the updated list to the view
            ViewBag.Transactions = _transactions;
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
