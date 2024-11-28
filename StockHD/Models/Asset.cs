using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public AssetType AssetType { get; set; } = new AssetType();
        [Display(Name = "Constructeur")]
        public string Manufacturer { get; set; } = "";
        [Display(Name = "Numéro de série")]
        public string? SerialNumber { get; set; } = "";
        public string? Description { get; set; } = "";
        public Location? Location { get; set; }
        [Display(Name = "Propriétés")]
        public Collection<ExtendedPropertyValue> PropertiesValues { get; set; } = new Collection<ExtendedPropertyValue>();
        
    }
}