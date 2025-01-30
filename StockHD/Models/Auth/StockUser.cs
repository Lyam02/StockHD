using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace StockHD.Models.Auth
{
    public class StockUser : IdentityUser
    {
        [PersonalData]
        [DataType(DataType.Text)]
        [Display(Name = "Nom complet")]
        public string Name { get; set; }

        [PersonalData]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Adresse Email")]
        public string Email { get; set; }

        [Display(Name = "Date de création")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
    }
}