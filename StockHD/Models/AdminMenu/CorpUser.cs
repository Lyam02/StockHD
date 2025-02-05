using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class CorpUser
    {
        public string CK { get; set; }
        [Display (Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Prénom")]
        public string Surname { get; set; }
        [Display(Name = "Adresse Mail")]
        public string EmailAddress { get; set; }
        [Display(Name = "Service")]
        public string Departement { get; set; }
        [Display(Name = "Date d'effet")]
        public string StartDate { get; set; }
    }
}