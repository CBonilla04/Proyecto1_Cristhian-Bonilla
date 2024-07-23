using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Models;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Controllers
{
    //permitir acceso solo a usuarios autenticados
    [Authorize]
    public class HistoricalController : Controller
    {
        private readonly IReservationService _reservationService;

        public HistoricalController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //muestra las reservaciones realizadas por el usuario (histórico)
        [HttpGet]
        public async Task<IActionResult> Historical(List<Reservations> reservations)
        {
            try
                {
                CurrentUser userSession = HttpContext.Session.GetObjectFromJson<CurrentUser>("CurrentUser");
                reservations = await _reservationService.GetReservationByUser(userSession.IdUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View(reservations);
        }
    }
}
