using Microsoft.AspNetCore.Mvc;

namespace StockHD.Controllers
{
    public class LocalisationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
