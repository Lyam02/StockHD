using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
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

        // Register

        

        
    }
}