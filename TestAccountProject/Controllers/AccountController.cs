using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using TestAccountProject.ViewModels;
using System.Text.Encodings.Web;
using System.Net.Mail;
using System.Net;
using TestAccountProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace TestAccountProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        public AccountController(SignInManager<IdentityUser> SManager, UserManager<IdentityUser> UManager, RoleManager<IdentityRole> RM, ILogger<AccountController> logger)
        {
            _userManager = UManager;
            _signInManager = SManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser { Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, "user");

                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callBackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    EmailService emailService = new EmailService();
                    await emailService.SendEmailAsync(model.Email, "Confirm your account", $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callBackUrl}'>link</a>");

                    return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return View();
            }

            ModelState.AddModelError("", "User couldnt comfirmed");
            return View(ModelState);
        }

       /* private async Task<bool> SendEmailAsync(string to, string subject, string confirmLink)
        {
            MailMessage message =  new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("theesanjar@gmail.com");
            message.To.Add(to);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = confirmLink;

            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("theesanjar@gmail.com", "Sanjar1203");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                _logger.LogInformation("Trying to send email");
                await smtpClient.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                _logger.LogInformation("Caught an exception");
                return false;
            }
        }*/

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View( new LoginModel { ReturnUrl = returnUrl});
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (isPasswordCorrect == true)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        if (Url.IsLocalUrl(model.ReturnUrl) && !string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied() => View();
    }
}
