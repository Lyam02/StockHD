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

        [HttpGet]
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
    }
}
