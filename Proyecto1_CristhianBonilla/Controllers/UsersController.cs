using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        private readonly IAmadeusApiService _amadeusApiService;

        public UsersController(IAmadeusApiService amadeusApiService, IUserService userService)
        {
            _amadeusApiService = amadeusApiService;
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> AddUser(Users user)
        {
            try
            {
                int restult = 0;
                user = await _userService.AddUser(user);

                if (user ==  null)
                {
                    ViewData["Message"] = "No se pudo guardar el usuario";
                    return View(user); 
                }
                return RedirectToAction("Login","LogIn");
            }
            catch (Exception ex)
            {

                ViewData["Message"] = ex.InnerException?.Message;
                return View(user);
            }

        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditUser()
        {
            CurrentUser user = HttpContext.Session.GetObjectFromJson<CurrentUser>("CurrentUser");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(CurrentUser user)
        {
            try
            {
                
                bool restul = await _userService.EditUser(user);

                if (restul)
                {
                    ViewData["Message"] = "No se pudo modificar el usuario";
                    return View(user);
                }
                return View(user);
            }
            catch (Exception ex)
            {

                ViewData["Message"] = ex.InnerException?.Message;
                return View(user);
            }

        }




    }

}
