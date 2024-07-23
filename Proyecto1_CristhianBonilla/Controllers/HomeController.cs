using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Services;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private Filters filters = new Filters();
        private readonly IHomeService _homeService;
        private readonly IAmadeusApiService _amadeusApiService;
        public HomeController(AppDbContext appDbContext, IHomeService homeService, IAmadeusApiService amadeusApiService)
        {
            _appDbContext = appDbContext;
            _homeService = homeService;
            _amadeusApiService = amadeusApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchAirports(string keyword)
        {
            await _homeService.AuthenticateAsync("0pYa9rS3KzA0aFecnSAgU8DUI3BOmBql", "bG6OnyjSJclDij03");
            var airports = await _homeService.GetAirportsAsync(keyword);
            return Json(airports);
        }
        //muestra los vuelos disponibles
        [HttpGet]
        public async Task<IActionResult> getFlights(Filters model)
        {
            try
            {
            //permite la autenticación en amadeus
            await _amadeusApiService.AuthenticateAsync("0pYa9rS3KzA0aFecnSAgU8DUI3BOmBql", "bG6OnyjSJclDij03");
            var flightOffers = await _amadeusApiService.GetFlightOffersAsync(
                    model.origin,
                    model.destination, 
                    model.departureDate.ToString("yyyy-MM-dd"), 
                    model.returnDate.ToString("yyyy-MM-dd"), 
                    model.adults.ToString(), 
                    model.children.ToString(), 
                    model.infants.ToString(), 
                    model.travelClass != null ? model.travelClass.ToString() : "",
                    model.notStop ? "true" : "false",
                    model.maxPrice >= 1 ? model.maxPrice.ToString() : "" 
                    );
            if(flightOffers == null)
            {
                ViewData["Message"] = "No se encontraron resultados";
            }
            return Json(flightOffers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new { error = ex.Message });
            }
        }
        //agrega un vuelo al carrito
        [HttpPost]
        public IActionResult AddToCart([FromBody] FlightOffer flight)
        {

            List<FlightOffer> carList = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
            if (carList == null)
            {
                carList = new List<FlightOffer>();
            }

            carList.Add(flight);
            HttpContext.Session.SetObjectAsJson("CartStore", carList);
            return Ok();

        }
        [HttpGet]
        public IActionResult GetCartItemCount()
        {
            List<FlightOffer> carList = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
            int itemCount = carList?.Count ?? 0;
            return Json(new { count = itemCount });
        }
    }

    


}
