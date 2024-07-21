using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IReservationOrder _reservationOrder;

        public PaymentController(IReservationOrder reservationOrder)
        {
            _reservationOrder = reservationOrder;
        }
        [HttpGet]
        public async Task<IActionResult> Payment()
        {
            List<FlightOffer> flights = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
            if (flights == null)
            {
                flights = new List<FlightOffer>();
            }
            CurrentUser userSession = HttpContext.Session.GetObjectFromJson<CurrentUser>("CurrentUser");
            await _reservationOrder.IntegrateReservation(flights, userSession.IdUser);

            return View(flights);
        }
    }
}
