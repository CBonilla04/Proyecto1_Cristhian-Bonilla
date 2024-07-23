﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;
using System.Text.RegularExpressions;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        private readonly IAmadeusApiService _amadeusApiService;

        //Es una expresión regular que valida que la contraseña tenga al menos 8 caracteres, un carácter especial, una letra mayúscula y un número
        string regexPattern = @"^(?=.*[!@#$%^&*(),.?\"":{}|<>-_;+])(?=.*\d).{8,}$";

        public UsersController(IAmadeusApiService amadeusApiService, IUserService userService)
        {
            _amadeusApiService = amadeusApiService;
            _userService = userService;
        }

        //muestra la vista de registro
        [HttpPost]
        public async Task<IActionResult> AddUser(UserView user)
        {
            HashData hashData = new HashData();
            try
            {
                if(user.IdUser == 0)
                {
                    ViewData["Message"] = "El campo de cédula no puede ser 0";
                    return View(user);
                }
                else if (user.Password != user.PasswordCheck)
                {
                    ViewData["Message"] = "Las contraseñas no coinciden";
                    return View(user);
                }
                else if (!Regex.IsMatch(user.Password, regexPattern))
                { 
                    ViewData["Message"] = "La contraseña debe tener al menos 8 caracteres, un carácter especial y un número";
                    return View(user);
                }
                else {
                    //guarda el usuario
                    Users userToSave = new Users
                    {
                        IdUser = user.IdUser,
                        Name = user.Name,
                        Surname = user.Surname,
                        SecondSurname = user.SecondSurname,
                        Age = user.Age,
                        Email = user.Email,
                        Password = hashData.HashPassword(user.Password),
                        Preferences = user.Preferences
                    };
                    await _userService.AddUser(userToSave);
                    if (userToSave == null)
                    {
                        ViewData["Message"] = "No se pudo guardar el usuario";
                        return View(user);
                    }
                    return RedirectToAction("Login", "LogIn");
                }   
                                            
            }
            catch (Exception ex)
            {

                ViewData["Message"] = ex.InnerException?.Message;
                return View(user);
            }

        }
        //muestra la vista de registro
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        //muestra la vista de edición de usuario
        [Authorize]
        [HttpGet]
        public ActionResult EditUser()
        {
            CurrentUser user = HttpContext.Session.GetObjectFromJson<CurrentUser>("CurrentUser");
            return View(user);
        }
        //muestra la vista de edición de usuario
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUser(CurrentUser user)
        {
            try
            {
                //modifica el usuario
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
