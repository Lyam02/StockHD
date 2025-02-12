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
        protected RoleManager<StockRole> _RoleManager { get; }
        public RolesController(UserManager<StockUser> userManager, RoleManager<StockRole> roleManager, StockDbContext context)
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
        public IActionResult Create_Role()
        {
            var role = new StockRole
            {
                Name = ""
            };

            return View(role);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_Role(StockRole _sRole)
        {
            if (!ModelState.IsValid)
            {
                return View(_sRole);
            }

            try
            {
                _context.Add(_sRole);

            }
        }

        //******************************************************
    }
}