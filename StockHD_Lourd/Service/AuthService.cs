using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockLibrary;
using MySqlX.XDevAPI;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;
using StockLibrary.Models.Auth;

namespace StockHD_Lourd.Service
{
    public class AuthService
    {
        private readonly StockDbContext _context;

        public AuthService(StockDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Vérifie les identifiants de l'utilisateur
        /// </summary>
        public bool Login(string username, string password)
        {
            string hashedPassword = HashPassword(password);

            var user = _context.Users
                               .FirstOrDefault(u => u.Email == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                Session.CurrentUser = new SignInUser
                {
                    Email = user.Email,
                    Password = user.PasswordHash
                };
                return true;
            }
            return false;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static class Session
        {
            public static SignInUser CurrentUser { get; set; }
        }

    }
}
