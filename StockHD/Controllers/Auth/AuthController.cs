﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using StockHD.Data;
using StockHD.Models;
using Microsoft.AspNetCore.Identity;
using StockHD.Models.Auth;

namespace StockHD.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly StockDbContext _context;
        protected UserManager<StockUser> _UserManager { get; }
        protected RoleManager<IdentityRole> _RoleManager { get; }
        protected SignInManager<StockUser> _SignInManager { get; }

        public AuthController(ILogger<AuthController> logger, UserManager<StockUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<StockUser> signInManager, StockDbContext context)
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
                var user = new StockUser { UserName = $"{rUser.Surname}.{rUser.Name}", Email = rUser.Email, Surname = rUser.Surname, Name = rUser.Name};
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

                if (user != null && await _UserManager.CheckPasswordAsync(user, sUser.Password)){
                    await _SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else{
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

    }
}