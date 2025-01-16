using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockHD.Data;
using StockHD.Models;
using System.Diagnostics;

namespace StockHD.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ILogger<PropertyController> _logger;
        private readonly StockDbContext _context;

        public PropertyController(ILogger<PropertyController> logger,StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // [ActionName("Index")]  // permet de raccoursir le nom du chemin dans le Web. Donc le RedirectToAction doit porter le même nom que ActionName
        // car c'est une redirection, ou alors utiliser un nameof et mettre le nom de la class (ici Indextest) 
        // public IActionResult Indextest()
        public async Task<IActionResult> Index()
        {

            var Properties = await _context.Properties.Include(e => e.AssetTypes).ToListAsync();


            return View(Properties);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //--------------------------------------------------

        // Create Properties

        //--------------------------------------------------

        public IActionResult Create_prop()
        {
            var properties = new ExtendedProperty
            {
                Name = "",
                Description = ""

            };
         
            return View(properties);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_prop(ExtendedProperty properties)
        {
            if (!ModelState.IsValid)
            {
                
                return View(properties);
            }

            _context.Add(properties);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //--------------------------------------------------

        // Edit Properties

        //--------------------------------------------------

        public async Task<IActionResult> Edit_prop(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var properties = await _context.Properties.SingleOrDefaultAsync(p => p.Id == id);
            if (properties == null)
            {
                return NotFound();
            }

            return View(properties);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_prop(ExtendedProperty properties)
        {
            if (!ModelState.IsValid)
            {
                return View(properties);
            }

            _context.Update(properties);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //--------------------------------------------------

        // Delete Properties

        //--------------------------------------------------

        public async Task<IActionResult> Delete_prop(int? id)
        {
            var properties = await _context.Properties.SingleOrDefaultAsync(p => p.Id == id);

            if (id == null)
            {
                return NotFound();
            }

           
            if(properties == null)
            {
                return NotFound();
            }

            return View(properties);
        }

        [HttpPost, ActionName("Delete_prop")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_prop(ExtendedProperty properties)
        {
            if (!ModelState.IsValid)
            {
                return View(properties);
            }

            _context.Remove(properties);
            await _context.SaveChangesAsync();
            //return RedirectToAction("Indextest"); // ici il faut que ça soit Index car le ActionName est Index
            //return RedirectToAction("Index");   // ici cela fonctionne car il a bien le même nom de redirection que ActioName
            return RedirectToAction(nameof(Index));
        }

    }
}