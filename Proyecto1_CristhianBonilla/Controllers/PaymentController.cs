using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IReservationOrder _reservationOrder;

        public PaymentController(IReservationOrder reservationOrder)
        {
            _reservationOrder = reservationOrder;
        }
        //muestra la vista de pago
        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            try
            {
                List<FlightOffer> flights = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
                if (flights == null)
                {
                    flights = new List<FlightOffer>();
                }
                CurrentUser userSession = HttpContext.Session.GetObjectFromJson<CurrentUser>("CurrentUser");
                await _reservationOrder.IntegrateReservation(flights, userSession.IdUser, userSession.Name);
                //elimina los vuelos pagados del carrito
                HttpContext.Session.Remove("CartStore");

                return View(flights);
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}
