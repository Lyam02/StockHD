using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLibrary;

namespace StockHD.Controllers
{
    public class StatController : Controller
    {
        private readonly StockDbContext _context;
        public StatController(StockDbContext context)
        {
            _context = context;
        }

        // GET: StatController
        public ActionResult Index()
        {
            var StatView = _context.Stat.ToList();
            return View(StatView);
        }

    }
}
