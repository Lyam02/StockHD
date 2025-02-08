using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using NuGet.ContentModel;
using StockHD.Data;
using StockHD.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StockHD.Controllers
{
    [Authorize]
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
        public IActionResult GetPropertiesCreate()
        {
            var properties = _context.Properties.ToList();

            return PartialView(properties);

        }


        private class Props
        {
            public List<int> Properties= new List<int>();
        }

        // POST : Type/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssetType assetType, string jsonPropCreate)
        {

            Props props =  JsonConvert.DeserializeObject<Props>(jsonPropCreate);

            assetType.Properties = new Collection<ExtendedProperty>();
            _context.Properties
                .AsEnumerable() // Switch to client-side evaluation
                .Where(p => props.Properties.Contains(p.Id))
                .ToList()
                .ForEach(assetType.Properties.Add);
            //_context.Properties.Where(p => props.Properties.Contains(p.Id)).ToList().ForEach(assetType.Properties.Add);

            //Manière non factorisé de faire :

            /*Props props = JsonConvert.DeserializeObject<Props>(jsonProp);

            List<ExtendedProperty> ExProps = _context.Properties.Where(p => props.Properties.Contains(p.Id)).ToList();

            foreach (var ExProp in ExProps)
            {
                assetType.Properties.Add(ExProp);
            }*/

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

            var AssetType = await _context.Types.Include(t=>t.Properties).SingleOrDefaultAsync(t => t.Id == id);
            if (AssetType == null) {

                return NotFound();
            }
            Prop();
            return View(AssetType);
        }

        [HttpGet]
        public IActionResult GetPropertiesEdit()
        {
            var properties = _context.Properties.ToList();

            return PartialView(properties);
        }

        //POST : Type/Edit  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AssetType assetType, string jsonPropEdit)
        {
            Props editProp = JsonConvert.DeserializeObject<Props>(jsonPropEdit);
            AssetType aType = await _context.Types.Include(t => t.Properties).SingleAsync(t => t.Id == assetType.Id);

           // assetType.Properties = new Collection<ExtendedProperty>();

            if (editProp != null && editProp.Properties.Count() > 0)
            {
                _context.Properties.Where(p => editProp.Properties.Contains(p.Id) && !aType.Properties.Contains(p)).ToList()
                    .ForEach(aType.Properties.Add);

                foreach (var prop in aType.Properties.ToList().Where(p => !editProp.Properties.Contains(p.Id)).ToList())
                {
                    aType.Properties.Remove(prop);
                }
            }
            else
            {
                foreach (var prop in aType.Properties.ToList())
                {
                    aType.Properties.Remove(prop);
                }
            }


            if (!ModelState.IsValid)
            {
                return View(aType);
            }

            _context.Update(aType);
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
            ViewData["ExtendedProperties"] = _context.Properties.ToList();
        }
    }
}