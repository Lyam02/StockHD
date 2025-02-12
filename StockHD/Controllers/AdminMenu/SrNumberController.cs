using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockLibrary;
using StockLibrary.Data;
using StockLibrary.Models;

namespace StockHD.Controllers.AdminMenu
{
    [Authorize]
    public class SrNumberController : Controller
    {

        private readonly ILogger<SrNumberController> _logger;
        private readonly StockDbContext _context;

        public SrNumberController(ILogger<SrNumberController> logger, StockDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: SerialNumber
        public ActionResult Index()
        {

            var SerialNo = _context.SrNumber.ToList();

            return View(SerialNo);

        }

        // Create SerialNumber
        /************************************************************/

        //GET

        public IActionResult Create_SrNumber()
        {
            var SerialNum = new SrNumber
            {
                SerialNumber = ""
            };

            return View(SerialNum);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_SrNumber(SrNumber SerialNo)
        {
            if (!ModelState.IsValid)
            {
                return View(SerialNo);
            }

            _context.Add(SerialNo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        /************************************************************/


        // DELETE SerialNumber
        /************************************************************/

        //GET
        [HttpGet]
        public async Task<IActionResult> Delete_SrNumber(string Srnumber)
        {
            if (Srnumber == null)
            {
                return NotFound();
            }

            var SN = await _context.SrNumber.FirstOrDefaultAsync(n => n.SerialNumber.Equals(Srnumber));

            if (SN == null)
            {
                return NotFound();
            }
            return View(SN);
        }

        //POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSrNumberConfirm(string SerialNumber)
        {
            SrNumber srnumber = await _context.SrNumber.FindAsync(SerialNumber);

            if (srnumber == null)
            {
                return NotFound();
            }

            _context.Remove(srnumber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /************************************************************/

        //EDIT SrNumber
        /************************************************************/

        //GET
        [HttpGet]
        public async Task<IActionResult> Edit_SrNumber(string Srnumber)
        {
            if (Srnumber == null)
            {
                return NotFound();
            }

            var srnumber = await _context.SrNumber.SingleOrDefaultAsync(s => s.SerialNumber.Equals(Srnumber));

            if(srnumber == null)
            {
                return NotFound();
            }

            return View(srnumber);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_SrNumber(SrNumber SerialNum)
        {
            if (!ModelState.IsValid)
            {
                return View(SerialNum);
            }

            _context.Update(SerialNum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /************************************************************/
    }
}
