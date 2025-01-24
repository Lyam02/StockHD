using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace StockHD.Models.Auth
{
    public class Auth : IdentityUser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nom complet")]
        public string UserName { get; set; }
 
        [Display(Name = "Date de création")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}