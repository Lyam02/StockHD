using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.ContentModel;
using StockHD.Data;
using StockHD.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace StockHD.Controllers
{
    public class TypeController : Controller
    {
        private readonly ILogger<TypeController> _logger;
        private readonly StockDbContext _context;

        public TypeController(ILogger<TypeController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
            //_logger.LogError("test err");
        }
        
        public IActionResult Index()
        {

            var Types = _context.Types.Include(e => e.Properties);


            return View(Types);
        }

        //--------------------------------------------------

        // Create AssetType

        //--------------------------------------------------

        // GET: Type/Create
        public IActionResult Create()
        {

            var assetType = new AssetType
            {
                Name = "",
                Description = "",

                
            };
            Prop();
            return View(assetType);

        }

        [HttpGet]
        public IActionResult GetProperties()
        {
            var properties = _context.Properties.ToList();
            
            return PartialView(properties);
            
        }


        // POST : Type/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetType assetType, int TypeSelect)
        {
            
            var selectedProperty = _context.Properties.SingleOrDefault(p => p.Id == TypeSelect);
            if (selectedProperty != null)
            {
                assetType.Properties = new Collection<ExtendedProperty> { selectedProperty };
            }
            else
            {
                assetType.Properties = new Collection<ExtendedProperty>();
            }

            if (!ModelState.IsValid)
            {
                Prop();
                return View(assetType);
            }

            _context.Add(assetType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //--------------------------------------------------

        // Edit AssetType

        //--------------------------------------------------


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AssetType = await _context.Types.SingleOrDefaultAsync(t => t.Id == id);
            if (AssetType == null) {

                return NotFound();
            }
            return View(AssetType);
        }

        //POST : Type/Edit  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AssetType assetType)
        {

            if (!ModelState.IsValid)
            {
                return View(assetType);
            }

            _context.Update(assetType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        //--------------------------------------------------

        // Delete AssetType

        //--------------------------------------------------

        public async Task<IActionResult> Delete(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var AssetType = await _context.Types.SingleOrDefaultAsync(t => t.Id == id);
            if (AssetType == null)
            {
                return NotFound();
            }

            return View(AssetType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AssetType assetType)
        {

            if (!ModelState.IsValid)
            {
                return View(assetType);
            }

            _context.Remove(assetType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        private void Prop()
        {
            ViewData["ExtendedProperty"] = _context.Properties.ToList();
        }
    }
}