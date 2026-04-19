using Microsoft.AspNetCore.Mvc;

namespace Rentaly.WebUI.Controllers
{
    public class BookingController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
