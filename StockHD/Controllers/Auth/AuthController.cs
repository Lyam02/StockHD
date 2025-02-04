using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StockHD.Data;
using StockHD.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using StockHD.Models.Auth;
using System.ComponentModel;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace StockHD.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly StockDbContext _context;
        protected UserManager<IdentityUser> _UserManager { get; }
        protected RoleManager<IdentityRole> _RoleManager { get; }
        protected SignInManager<IdentityUser> _SignInManager { get; }

        public AuthController(ILogger<AuthController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager, StockDbContext context)
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
                var user = new IdentityUser { UserName = $"{rUser.Surname}.{rUser.Name}", Email = rUser.Email };
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignInUser(SignInUser sUser)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync(sUser.Email);

                if (user != null && await _UserManager.CheckPasswordAsync(user, sUser.Password)){
                    await _SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else{
                    ModelState.AddModelError(string.Empty, "Une Erreur est survenu lors de la connexion");
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




    }
}