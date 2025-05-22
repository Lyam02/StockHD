using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using StockLibrary.Data;

namespace StockLibrary.Models.Auth
{
    // Modele pour les Sign Up
    public class RegistrationUser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Prénom")]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Adresse Email")]
        public string Email { get; set; }

        [PersonalData]
        [Display(Name = "Phrase secrète")]
        public string? SecretSentense { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Les deux mots de passe ne correspondent pas !")]
        public string ConfirmPassword { get; set; }
    }

    // Modele pour les Sign In
    public class SignInUser
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }
    }

    public class MV_RoleUsers
    {
        public StockRole Role { get; set; }
        [Display(Name = "Utilisateurs")]
        public List<StockUser> Users { get; set; }
    }

    public class AddUser
    {
        public string Username { get; set; }
    }

    public class ConfirmResetPassword
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string SecretSentence { get; set; }
    }

    public class ResetPassword
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau Mot de passe")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation Nouveau Mot de passe")]
        [Compare("Password", ErrorMessage = "Les deux mots de passe ne correspondent pas !")]
        public string ConfirmPassword { get; set; }
    }
}