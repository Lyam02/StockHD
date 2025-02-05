using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class SrNumber
    {
        [Display(Name = "Numéro de série")]
        public string? SerialNumber { get; set; }
        public string? Description { get; set; }

    }
}