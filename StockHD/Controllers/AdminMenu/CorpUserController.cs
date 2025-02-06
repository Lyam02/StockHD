﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using StockHD.Data;
using StockHD.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace StockHD.Controllers.AdminMenu
{
    public class CorpUserController : Controller
    {
        private readonly ILogger<CorpUserController> _logger;
        private readonly StockDbContext _context;

        public CorpUserController(ILogger<CorpUserController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: SerialNumber
        public ActionResult Index()
        {

            var cUser = _context.CorpUser.ToList();

            return View(cUser);

        }

        //CREATE CorpUser
        /*********************************************************/

        //GET
        public IActionResult Create_CorpUser()
        {
            var corpUser = new CorpUser
            {
                CK = "",
                Surname = "",
                Name = "",
                EmailAddress = "",
                Departement = "",
                StartDate = ""
            };

            return View(corpUser);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_CorpUser(CorpUser _cUser)
        {
            if (!ModelState.IsValid)
            {
                return View(_cUser);
            }

            try
            {
                _context.Add(_cUser);
                await _context.SaveChangesAsync();
               

            }catch (Exception ex)
            {
                
            }
            return RedirectToAction(nameof(Index));
        }

        /*********************************************************/


        //DELETE CorpUser
        /*********************************************************/

        //GET

        public async Task<IActionResult> Delete_CorpUser (string ck)
        {
            if (ck == null)
            {
                return NotFound();
            }

            var CorpKey = await _context.CorpUser.FirstOrDefaultAsync(c => c.CK.Equals(ck));

            if(CorpKey == null)
            {
                return NotFound();
            }

            return View(CorpKey);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCorpuserConfirm (string Ck)
        {
            var cKey = await _context.CorpUser.FindAsync(Ck);

            if(cKey == null)
            {
                return NotFound();
            }

            _context.Remove(cKey);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*********************************************************/


        //EDIT CorpUser
        /*********************************************************/

        //GET
        public async Task<IActionResult> Edit_CorpUser(string ck)
        {
            if (ck == null)
            {
                return NotFound();
            }

            var cUser = await _context.CorpUser.SingleOrDefaultAsync(c => c.CK.Equals(ck));

            if(cUser == null)
            {
                return NotFound();
            }

            return View(cUser);

        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_CorpUser(CorpUser corpUser)
        {
            if (!ModelState.IsValid)
            {
                return View(corpUser);
            }

            _context.Update(corpUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /*********************************************************/
    }
}