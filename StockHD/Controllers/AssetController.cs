using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockHD.Data;
using StockHD.Models;
using System.Diagnostics;

namespace StockHD.Controllers
{
    public class AssetController : Controller
    {
        private readonly ILogger<AssetController> _logger;
        private readonly StockDbContext _context;

        public AssetController(ILogger<AssetController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public ActionResult Index()
        {
            var Assets = _context.Assets.Include(t => t.AssetType);

            return View(Assets);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //--------------------------------------------------

        // Create Asset

        //--------------------------------------------------

        public IActionResult Create_Asset()
        {
            var Asset = new Asset
            {
                Manufacturer = "",

                SerialNumber = "",
                Description = "",
            };
            Type();

            return View(Asset);

        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Asset(Asset asset, int AssetTypeSelect)
        {
            asset.AssetType = _context.Types.SingleOrDefault(t => t.Id == AssetTypeSelect)!;
            if (!ModelState.IsValid)
            {
                Type();
                return View(asset);
            }

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        /*
         --------------------------------------

          Delete Asset

         --------------------------------------
         */

        public async Task<IActionResult> Delete_Asset(int? id)
        {
            var asset = await _context.Assets.FirstOrDefaultAsync(x => x.Id == id);

            if (id == null)
            {
                return NotFound();
            }

            if (asset == null)
            {
                return NotFound();
            }

            Type();
            return View(asset);
        }

        [HttpPost, ActionName("Delete_Asset")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Asset(int Id)
        {

            Asset? asset = await _context.Assets.FindAsync(Id);

            if (asset == null)
            {
                return NotFound();
            }

            _context.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        private void Type()
        {
            ViewData["AssetTypes"] = _context.Types.ToList();
            ViewData["ExtendedProperty"] = _context.PropertiesValues.ToList();
        }
    }
}