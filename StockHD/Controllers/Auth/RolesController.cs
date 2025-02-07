using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockHD.Data;
using StockHD.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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
    }
}