using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class CorpUser
    {
        [Required]
        [Display(Name = "Corporate Key" )]
        public string CK { get; set; }
        [Required]
        [Display(Name = "Prénom")]
        public string Surname { get; set; }
        [Required]
        [Display (Name = "Nom")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "Adresse Mail")]
        public string EmailAddress { get; set; }
        [Display(Name = "Service")]
        public string Departement { get; set; }
        [Display(Name = "Date d'effet")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public string? StartDate { get; set; }
    }
}