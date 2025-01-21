using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using StockHD.Data;
using StockHD.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
            var assets = _context.Assets.Include(t => t.AssetType).Include(l => l.Location).ToList();

            return View(assets);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //--------------------------------------------------

        // Create Asset

        //--------------------------------------------------

        //GET

        public async Task<IActionResult> GetPropertiesEx(int Id = 0)
        {
            if (Id == 0) return NotFound();

            var aType = await _context.Types.Include(p => p.Properties).SingleOrDefaultAsync(t => t.Id == Id);

            if (aType == null) return NotFound();

            List<ExtendedPropertyValue> pValues = aType.Properties.Select(p => new ExtendedPropertyValue() { Property = p, Value = "" }).ToList();
            /*
            Ici on met dans une liste les ExtendedPropertyValue de chaque Objet Properties qui se trouve dans aType.                        
            */

            return PartialView(pValues);
        }

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

        public class PropValue
        {
            public int Id;
            public string Value = "";
        }

        //POST Asset
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Asset(Asset asset, int AssetTypeSelect, int LocationSelect, string jsonPropValCreate)
        {

            List<PropValue> pValues = JsonConvert.DeserializeObject<List<PropValue>>(jsonPropValCreate);

            asset.PropertiesValues = new Collection<ExtendedPropertyValue>();
            
            foreach (var propValues in pValues)
            {

                ExtendedPropertyValue extendedPropValues = new ExtendedPropertyValue
                {
                    Property = _context.Properties.SingleOrDefault(p=>p.Id == propValues.Id),
                    Value = propValues.Value,
                };

                asset.PropertiesValues.Add(extendedPropValues);
                // await _context.SaveChangesAsync();
            }
                
            /*_context.PropertiesValues.Where(p => pValues.Contains(v => v.Id == p.Id)).ToList().ForEach(asset.PropertiesValues.Add);*/

            

            asset.Location = _context.Locations.SingleOrDefault(l => l.Id == LocationSelect)!;

            asset.AssetType = _context.Types.SingleOrDefault(t => t.Id == AssetTypeSelect)!;
            if (!ModelState.IsValid)
            {
                Type();
                return View(asset);
            }

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync(); //Sauvegarde des changements

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

        //-----------------------

        // Edit Asset

        //-----------------------

        //GET
        public async Task<IActionResult> Edit_Asset(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Asset = await _context.Assets.SingleOrDefaultAsync(a => a.Id == id);

            if (Asset == null)
            {
                return NotFound();
            }

            Type();
            return View(Asset);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Asset(Asset asset, int AssetTypeSelect, int LocationSelect, int id)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (AssetTypeSelect > 0)
                    {
                        AssetType type = _context.Types.SingleOrDefault(t => t.Id == AssetTypeSelect);
                        
                        if (type != null)
                        {
                            asset.AssetType = type;
                        }
                    }
                    
                    if (LocationSelect > 0)
                    {
                        Location location = _context.Locations.SingleOrDefault(l => l.Id == LocationSelect);

                        if (location != null)
                        {
                            asset.Location = location;
                        }
                    }
                    _context.Update(asset);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));


                /*if (!ModelState.IsValid)
                {
                    return View(asset);
                }

                _context.Update(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");*/
            }
            Type();
            return View(asset);
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }

        private void Type()
        {
            ViewData["Locations"] = _context.Locations.ToList();
            ViewData["AssetTypes"] = _context.Types.ToList();
        }
    }
}