using Microsoft.AspNetCore.Mvc;

namespace StockHD.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
