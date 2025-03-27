using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StockLibrary;
using StockLibrary.Data;
using StockLibrary.Models;
using StockLibrary.Models.Auth;

namespace StockHD.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly StockDbContext _context;
        protected UserManager<StockUser> _UserManager { get; }
        protected RoleManager<StockRole> _RoleManager { get; }
        protected SignInManager<StockUser> _SignInManager { get; }

        public AuthController(
            ILogger<AuthController> logger,
            UserManager<StockUser> userManager,
            RoleManager<StockRole> roleManager,
            SignInManager<StockUser> signInManager,
            StockDbContext context
        )
        {
            _logger = logger;
            _RoleManager = roleManager;
            _UserManager = userManager;
            _SignInManager = signInManager;
            _context = context;
        }




        // Sign Up
        //***************************************************************************************//
        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegistrationUser rUser)
        {
            if (ModelState.IsValid)
            {
                var user = new StockUser
                {
                    UserName = $"{rUser.Surname}.{rUser.Name}",
                    Email = rUser.Email,
                    Surname = rUser.Surname,
                    Name = rUser.Name,
                    SecretSentense = rUser.SecretSentense
                };
                var result = await _UserManager.CreateAsync(user, rUser.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignInUser", "Auth");
                }
            }
            return View(rUser);
        }

        //***************************************************************************************//

        // Sign In
        //***************************************************************************************//
        [HttpGet]
        public async Task<IActionResult> SignInUser()
        {
            /*if (!_context.Roles.Any())*/ await CreateUserRole();
            

            ViewData["ErrMsg"] = "";
            return View();
        }

        private async Task<bool> CreateUserRole()
        {
            if (!_context.Roles.Any())
            {
                foreach (string roleName in new List<string> { "Admin", "Proximité", "IAM", "Invité" })
                {
                    var result = await _RoleManager.CreateAsync(new StockRole { Name = roleName });
                }

                // (new List<string> { "Admin", "Proximité", "IAM", "Invité" }).ForEach(n => _RoleManager.CreateAsync(new StockRole { Name = n }));

            }

            if (!_context.Users.Any())
            {
                var result = await _UserManager.CreateAsync(new StockUser { Name = "Admin", Surname = "Admin", SecretSentense = "Admin", Email = "Admin@ing.me", UserName = "Admin" }, "Admin123#");
                if (result.Succeeded)
                {
                    var result2 = await _UserManager.AddToRoleAsync(await _UserManager.FindByNameAsync("Admin"), "Admin");
                }
            }

            //List<StockRole> roles = new List<StockRole>();

            //roles.Add(new StockRole { Name = "Admin" });
            //roles.Add(new StockRole { Name = "Proximité" });
            //roles.Add(new StockRole { Name = "IAM" });
            //roles.Add(new StockRole { Name = "Invité" });

            //if (!_context.Roles.Any())
            //{
            //    foreach (StockRole stockRole in roles)
            //    {
            //        var role = new StockRole { Name = stockRole.Name };
            //        var result = await _RoleManager.CreateAsync(role);
            //    }   
            //}

            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInUser(SignInUser sUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(sUser.Email);

                if (user != null && await _UserManager.CheckPasswordAsync(user, sUser.Password))
                {
                    await _SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrMsg"] = "Une Erreur est survenu, vérifiez vos identifiants";
                    // ModelState.AddModelError(string.Empty, "Une Erreur est survenu lors de la connexion");
                }
            }
            return View(sUser);
        }

        // Logout
        //***************************************************************************************//
        public async Task<IActionResult> LogoutUser()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("SignInUser", "Auth");
        }

        //***************************************************************************************//

        // Confirmer reset mdp
        //***************************************************************************************//

        public IActionResult ConfirmResetPassWord()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassWord (string email, string secretSentence)
        {
            var user = await _UserManager.FindByEmailAsync(email);
            if (user != null && user.SecretSentense == secretSentence)
            {
                return RedirectToAction("ChangePassword");
            }
            return RedirectToAction("ConfirmResetPassWord");
        }

        //***************************************************************************************//

        // changer mdp
        //***************************************************************************************//

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string email, string password)
        {
            var user = await _UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _UserManager.GeneratePasswordResetTokenAsync(user);
                var result = await _UserManager.ResetPasswordAsync(user, token, password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignInUser");
                }
            }
            return RedirectToAction("ChangePassword");
        }

    }
}
