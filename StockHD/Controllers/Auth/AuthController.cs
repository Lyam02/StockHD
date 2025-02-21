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
        public IActionResult SignInUser()
        {
            ViewData["ErrMsg"] = "";
            return View();
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

        // Forgot Password
        //***************************************************************************************//

        public IActionResult ResetMDP()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RestMDP(string Email, string Password, string ConfirmPassword)
        {
            return View(null);
            //var model = new ResetMDP { Email = email };

            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync( Email);
                if (user != null)
                {
                    // Si l'utilisateur existe, on le redirige vers la page de réinitialisation de mot de passe
                    return RedirectToAction("ResetPassword", new { email = Email });
                }
                // Si l'email n'existe pas, on peut afficher un message d'erreur ou laisser l'utilisateur réessayer
                ModelState.AddModelError(string.Empty, "Aucun utilisateur trouvé avec cette adresse e-mail.");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string Email, string Password, string ConfirmPassword)
        {
            return View(null);
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(Email);
                if (user != null)
                {
                    var result = await _UserManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _UserManager.AddPasswordAsync(user, Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("ResetPasswordConfirmation");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "L'email fourni est incorrect.");
                }
            }

            return View();
        }
    }
}
