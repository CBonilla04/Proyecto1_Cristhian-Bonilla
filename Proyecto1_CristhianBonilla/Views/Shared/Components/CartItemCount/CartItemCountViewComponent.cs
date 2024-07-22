using Microsoft.AspNetCore.Mvc;
using Proyecto1_CristhianBonilla.Utils;
using Proyecto1_CristhianBonilla.ViewModels;

namespace Proyecto1_CristhianBonilla.Views.Shared.Components.CartItemCount
{
    public class CartItemCountViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var flights = HttpContext.Session.GetObjectFromJson<List<FlightOffer>>("CartStore");
            int itemCount = flights?.Count ?? 0;
            ViewData["CartItemCount"] = itemCount;
            return View(itemCount);
        }
    }
}
