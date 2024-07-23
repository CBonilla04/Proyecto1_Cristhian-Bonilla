using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        public LoginController(AppDbContext appDbContext, IUserService userService, IEmailSender emailSender)
        {
            _appDbContext = appDbContext;
            _userService = userService;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Users user)
        {
            try
            {                
                var login = await _userService.GetUser(user, HttpContext);
                if (login != null)
                {
                    await _emailSender.SendEmailLogin("Inicio de sesión", login);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Message"] = "Usuario o contraseña incorrectos";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Usuario o contraseña incorrectos." + ex.InnerException?.Message;
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            // Clear all session data
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the Login view
            return RedirectToAction("LogIn");
        }


    }
}
