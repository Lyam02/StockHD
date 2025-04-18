using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLibrary;

namespace StockHD.Controllers.Auth
{
    public class AdminCenterController : Controller
    {

        private readonly ILogger<AdminCenterController> _logger;
        
        public AdminCenterController (ILogger<AdminCenterController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

    }
}
