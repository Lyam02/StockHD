using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockLibrary;
using StockLibrary.Data;
using StockLibrary.Models;
using System.Data.Entity;
using System.Diagnostics;


namespace StockHD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StockDbContext _context;

        public HomeController(ILogger<HomeController> logger,StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var query = @"
            SELECT 
    at.Name AS AssetTypeName, 
    COUNT(a.Id) AS AssetCount,
    SUM(CASE WHEN l.Name = 'Stock' THEN 1 ELSE 0 END) AS StockAssetCount
FROM 
    AssetType at
LEFT JOIN 
    Assets a ON a.AssetTypeId = at.Id
LEFT JOIN 
    Locations l ON a.LocationId = l.Id
GROUP BY 
    at.Name
HAVING 
    (StockAssetCount = 0)
    OR COUNT(a.Id) = 0
    OR StockAssetCount = 1
    OR StockAssetCount > 1;

        "
            ;

            var alertAssets = _context.AlertAssets
                                  .FromSqlRaw(query)
                                  .ToList();

            return View(alertAssets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}