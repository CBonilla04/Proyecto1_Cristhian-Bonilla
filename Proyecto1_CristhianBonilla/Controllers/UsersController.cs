using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Utils;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class UsersController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(Users user)
        {
            try
            {
                await _appDbContext.Users.AddAsync(user);
                int restult = await _appDbContext.SaveChangesAsync();

                if (restult == 0)
                {
                    ViewData["Message"] = "No se pudo guardar el usuario";
                    return View(user); 
                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {

                ViewData["Message"] = ex.InnerException?.Message;
                return View(user);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Login(Users user)
        {
            try
            {
                var userLogin = await _appDbContext.Users.Where(u => u.IdUser == user.IdUser && u.Password == user.Password).FirstOrDefaultAsync();
                if (userLogin != null)
                {
                    return RedirectToAction("AddUser");
                }
                ViewData["Message"] = user.IdUser != 0 ? "Usuario o contraseña incorrectos" :  null;
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Usuario o contraseña incorrectos." + ex.InnerException?.Message; ;
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

    }

}
