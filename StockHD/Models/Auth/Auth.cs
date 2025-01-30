using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace StockHD.Models.Auth
{
    public class Authentication : IdentityUser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom complet")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confitmrt le mot de passe")]
        [Compare("Password", ErrorMessage = "Les deux mots de passe ne correspondent pas !")]
        public string ConfirmPassword { get; set; }
    }
}