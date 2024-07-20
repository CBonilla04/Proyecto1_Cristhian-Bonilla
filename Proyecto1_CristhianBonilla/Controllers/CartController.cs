﻿using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        public IActionResult Cart() 
        {
            List<FlightOffer> flights = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
            if (flights == null)
            {
                flights = new List<FlightOffer>();
            }
            return View(flights);
        }
        [HttpPost]
        public IActionResult Eliminar(int index)
        {
            List<FlightOffer> flights = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
            flights.RemoveAt(index);
            HttpContext.Session.SetObjectAsJson("CartStore", flights);
            return RedirectToAction("Cart");
        }
    }
}
