using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockLibrary;
using StockLibrary.Data;
using StockLibrary.Models.Auth;
using SQLitePCL;
using System.Data.Entity;
using System.Globalization;
using StockLibrary.Models;

namespace StockHD.Controllers.Auth
{
    public class AdminCenterController : Controller
    {

        private readonly ILogger<AdminCenterController> _logger;
        protected UserManager<StockUser> _UserManager { get; }
        private readonly StockDbContext _context;

        public AdminCenterController (ILogger<AdminCenterController> logger, UserManager<StockUser> userManager, StockDbContext context)
        {
            _logger = logger;
            _UserManager = userManager;
            _context = context;
        }
        
        

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> UserIndex()
        {
            var user = _context.Users.ToList();
            role();
            return View(user);
        }

        // Create User
        //***************************************************************************************//
        [HttpGet]
        public IActionResult Create_User()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create_User(RegistrationUser rUser)
        {
            if (ModelState.IsValid)
            {
                var user = new StockUser
                {
                    UserName = $"{rUser.Surname}.{rUser.Name}",
                    Email = rUser.Email,
                    Surname = rUser.Surname,
                    Name = rUser.Name,
                    SecretSentense = ""
                };

                var result = await _UserManager.CreateAsync(user, rUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserIndex");
                }
            }
            return View(rUser);
        }


        // Delete User
        //***************************************************************************************//
        public async Task<IActionResult> Delete_User(string id="")
        {

            if (id == "") return NotFound();

            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            role();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_UserConfirm(string id = "")
        {
            var delUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (User.Identity.Name == delUser.Name)
            {
                return PartialView();
            }

            if (id == "") return NotFound();

            _context.Remove(delUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserIndex");
        }

        // Detail User
        //***************************************************************************************//

        public async Task<IActionResult> Detail_User(string id = "")
        {
            if (id == "") return NotFound();

            var dUser = _context.Users.SingleOrDefault(d => d.Id == id);

            if (dUser == null) return NotFound();

            role();
            return View(dUser);
        }

        public void role()
        {
            ViewData["UserRoles"] = _context.UserRoles.ToList();
            ViewData["Roles"] = _context.Roles.ToList();
        }

        public async Task<IActionResult> Add_RoleUser(string id = "")
        {
            if (id == "") return NotFound();

            var user = _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_RoleUser(string id="", string selectRole="")
        {
            StockRole role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == selectRole);
            var user = _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (id == "" || selectRole == "")
            {
                return View();
            }

            await _UserManager.AddToRoleAsync(user, role.Name);
        }

    }
}
