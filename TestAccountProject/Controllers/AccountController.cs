using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TestAccountProject.Models;
using TestAccountProject.ViewModels;


namespace TestAccountProject.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext db;

        public AccountController(AppDbContext _context)
        {
            db = _context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel Model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == Model.Email);
                if (user is null)
                {
                    db.Users.Add(new User { Email = Model.Email, Password = Model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(Model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect data");
                }
            }
            return View(Model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel Model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == Model.Email && u.Password == Model.Password);
                if (user != null)
                {
                    await Authenticate(Model.Email);

                    return RedirectToAction("Index", "Home");
                }
                
                ModelState.AddModelError("", "Incorrect personal email or password");    
            }
            return View(Model);
        }

        public async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
