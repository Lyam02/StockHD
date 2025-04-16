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
    [Authorize(Roles = "Admin")]
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

        public ActionResult Detail(string Id)
        {
            var role = _context.Roles.SingleOrDefault(r=>r.Id == Id);
            var userIds = _context.UserRoles.Where(r => r.RoleId == Id) 
                                            .Select(r=>r.UserId).ToList();
            MV_RoleUsers roleUsers = new MV_RoleUsers
            {
                Role = role,
                Users = new List<StockUser>()
            };

            if(userIds.Count()> 0)
            {
                var users = _context.Users.AsEnumerable().Where(u => userIds.Contains(u.Id)).ToList();
                roleUsers.Users = users;
            }
                                //.Join(_context.Roles
                                //    , ur => ur.RoleId
                                //    , ur => ur.Id
                                //    , (ur, r) => new { RoleId = ur.RoleId, RoleName = r.Name,UserId = ur.UserId })
                                //.Join(_context.Users
                                //    , ur => ur.UserId
                                //    , ur => ur.Id
                                //    , (ur, u) => new { RoleId = ur.RoleId, RoleName = ur.RoleName, UserId = ur.UserId, UserName = u.UserName })
                                //.ToList(); 
            return View(roleUsers);
        }

        /*// CREATE ROLE
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
        public async Task<IActionResult> Create_Role(StockRole _sRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new StockRole { Name = _sRole.Name };
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
*/

        //AddUser
        //******************************************************

        [HttpGet]
        public IActionResult AddUser(string RoleId)
        {
            rUser();
            return View(_context.Roles.SingleOrDefault(r => r.Id == RoleId));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(string roleId, string rUserSelect)
        {
            StockUser user = await _context.Users.SingleOrDefaultAsync(u => u.Id == rUserSelect);
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId);

            if (user == null || role == null) 
            {
                rUser();
                return View();
            }

            await _UserManager.AddToRoleAsync(user, role.Name);
            return RedirectToAction(nameof(Index));
        }


        //******************************************************

        //RemoveUser
        //******************************************************

        [HttpGet]
        public IActionResult RemoveUser(string RoleId)
        {
            rUser();
            return View(_context.Roles.SingleOrDefault(r => r.Id == RoleId));
        }

        [HttpPost]
       /* public Task<IActionResult> RemoveUser()
        {

        }*/

        //******************************************************

        public void rUser()
        {
            ViewData["Users"] = _context.Users.ToList();

        }
    }
}