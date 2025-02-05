using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockHD.Data;
using StockHD.Models;

namespace StockHD.Controllers.AdminMenu
{
    [Authorize]
    public class SrNumberController : Controller
    {

        private readonly ILogger<SrNumberController> _logger;
        private readonly StockDbContext _context;

        public SrNumberController(ILogger<SrNumberController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: SerialNumber
        public ActionResult Index()
        {

            var SerialNo = _context.SrNumber.ToList();

            return View(SerialNo);

        }
    }
}
