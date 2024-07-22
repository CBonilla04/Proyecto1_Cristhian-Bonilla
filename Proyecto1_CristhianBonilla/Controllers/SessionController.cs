using Microsoft.AspNetCore.Mvc;

namespace Proyecto1_CristhianBonilla.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Expired()
        {
            return View();
        }
    }
}
