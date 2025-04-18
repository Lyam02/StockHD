using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StockHD.Controllers.AdminMenu;
using StockLibrary;

namespace StockHD.Controllers
{
    public class SheetsController : Controller
    {
        private readonly ILogger<SheetsController> _logger;
        private readonly StockDbContext _context;

        public SheetsController(ILogger<SheetsController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        
    }
}
