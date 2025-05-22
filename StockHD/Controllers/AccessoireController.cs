using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StockLibrary.Data;
using StockLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using StockLibrary;
using Humanizer;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace StockHD.Controllers
{
    public class AccessoireController : Controller
    {
        private readonly ILogger<AccessoireController> _Logger;
        private readonly StockDbContext _context;

        public AccessoireController(ILogger<AccessoireController> logger, StockDbContext context)
        {
            _Logger = logger;
            _context = context;
        }
        
        public IActionResult Index()
        {
            var accessoire = _context.Accessoire.ToList();

            return View(accessoire);
        }

        //_____________________________________________________________________________

        //CREATE Accessoire

        //_____________________________________________________________________________

        public IActionResult Create_Accessoire()
        {
            var Accessoire = new Accessoire();

            return View(Accessoire);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create_Accessoire(Accessoire a)
        {
            if (!ModelState.IsValid)
            {
                return View(a);
            }

            _context.Add(a);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //_____________________________________________________________________________

        //ASSIGN Accessoire

        //_____________________________________________________________________________

        public IActionResult Assign_Accessoire(string id = "")
        {
            var acc = _context.Accessoire.SingleOrDefault(a => a.Name == id);

            if (acc == null) return NotFound();

            var assingAccessoire = new AccessoireAssignement()
            {
                Accessoire = acc
            };

            Access();
            return View(assingAccessoire);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Assign_Accessoire(AccessoireAssignement accessoireAssign, string CorpUserSelect, string AccessoireName, int AccessoireQuantite)
        {

            accessoireAssign.CorpUser = _context.CorpUser.SingleOrDefault(c => c.CK == CorpUserSelect);
            accessoireAssign.Accessoire = _context.Accessoire.SingleOrDefault(a => a.Name == AccessoireName);
            accessoireAssign.Accessoire.Quantite = AccessoireQuantite - accessoireAssign.Quantite;

            _context.Add(accessoireAssign);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
            }
            return RedirectToAction(nameof(Index));

        }

        //_____________________________________________________________________________

        //DELETE Accessoire

        //_____________________________________________________________________________

        public IActionResult Delete_Accessoire(string id = "")
        {

            if (id == "")
            {
                return NotFound();
            }
            
            var accessoireDel = _context.Accessoire.SingleOrDefault(a => a.Name == id);

            if (accessoireDel == null)
            {
                return NotFound();
            }

            return View(accessoireDel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_AccessoireConfirm(string Name)
        {
            Accessoire accessoire = await _context.Accessoire.FindAsync(Name);

            if (accessoire == null)
            {
                return NotFound();
            }

            _context.Remove(accessoire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //_____________________________________________________________________________

        //EDIT Accessoire

        //_____________________________________________________________________________

        public async Task<IActionResult> Edit_Accessoire(string id = "")
        {
            /*var acc = await _context.Accessoire.SingleOrDefaultAsync(ac => ac.Name == id);*/

            if (id == "")
            {
                return NotFound();
            }

            var accessoire = await _context.Accessoire.SingleOrDefaultAsync(a => a.Name == id);

            if (accessoire == null)
            {
                return NotFound();
            }

            return View(accessoire);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Accessoire(Accessoire accessoire/*, string Name*/)
        {

            /*accessoire.Name = _context.Accessoire.SingleOrDefault(a => a.Name == Name);*/

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Edit_Accessoire));
            }

            _context.Update(accessoire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public void Access()
        {
            ViewData["CorpUser"] = _context.CorpUser.ToList();
            ViewData["Accessoire"] = _context.Accessoire.ToList();
        }
    }
}