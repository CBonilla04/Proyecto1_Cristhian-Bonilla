using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.Services;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserService _userService;
        public LoginController(AppDbContext appDbContext, IUserService userService)
        {
            _appDbContext = appDbContext;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(Users user)
        {
            try
            {
                var login = await _userService.GetUser(user, HttpContext);
                if (login != null)
                {
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

            // Redirect to the Login view
            return RedirectToAction("LogIn");
        }


    }
}
