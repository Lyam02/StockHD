using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockLibrary.Data;
using StockLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StockLibrary;
using StockLibrary.Models.Auth;

namespace StockHD.Controllers.Auth
{
    [Authorize]
    public class RolesController : Controller
    {
        private readonly StockDbContext _context;
        protected UserManager<StockUser> _UserManager { get; }
        protected RoleManager<IdentityRole> _RoleManager { get; }
        public RolesController(UserManager<StockUser> userManager, RoleManager<IdentityRole> roleManager, StockDbContext context)
        {
            _UserManager = userManager;
            _RoleManager = roleManager;
            /*_SignInManager = signInManager;*/
            _context = context;
        }
        
        //GET
        public ActionResult Index()
        {
            var Role = _context.Roles.ToList();
            return View(Role);
        }

        // CREATE ROLE
        //******************************************************

        //GET
        [HttpGet]
        public IActionResult Create_Role()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Role(IdentityRole _sRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole { Name = _sRole.Name };
                    var result = await _RoleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }

            }catch (Exception ex)
            {

            }
            
            return View (_sRole);
        }

        //DELETE ROLES
        //******************************************************

        //GET
        public async Task<IActionResult> Delete_Role (string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var _rId = await _context.Roles.FirstOrDefaultAsync(r => r.Id.Equals(Id));

            if (_rId == null)
            {
                return NotFound();
            }
            return View(_rId);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteRole (string Id)
        {
            var dRole = await _context.Roles.FindAsync(Id);

            if (Id == null)
            {
                return NotFound();
            }

            _context.Remove(dRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //******************************************************
    }
}