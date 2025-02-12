using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StockLibrary;
using StockLibrary.Data;
using StockLibrary.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StockHD.Controllers
{
    [Authorize]
    public class LocalisationController : Controller
    {
        private readonly ILogger<LocalisationController> _logger;
        private readonly StockDbContext _context;

        public LocalisationController(ILogger<LocalisationController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public ActionResult Index()
        {
            var Localisation = _context.Locations.Include(t => t.Assets).ToList();

            return View(Localisation);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //----------------------------

        // Create Location

        //----------------------------

        //GET

        public IActionResult Create_Loc()
        {
            var Location = new Location
            {
                Name = "",
                Description = "",
            };

            return View(Location);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create_Loc(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            _context.Add(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //------------------

        // Delete Location

        //------------------

        //GET

        public async Task<IActionResult> Delete_Loc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Location = await _context.Locations.FirstOrDefaultAsync(l => l.Id == id);
            if (Location == null)
            {
                return NotFound();
            }
            return View(Location);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Loc(int Id)
        {
            Location? location = await _context.Locations.FindAsync(Id);

            if (location == null)
            {
                return NotFound();
            }

            _context.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //------------------

        // Edit Location

        //------------------

        //GET
        public async Task<IActionResult> Edit_Loc(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var location = await _context.Locations.SingleOrDefaultAsync(l => l.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit_Loc(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            _context.Update(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }
    }
}
