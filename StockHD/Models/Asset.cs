using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class Asset
    {
        public int Id { get; set; }
        //[Display(AssetTypes = "Types" )]
        public AssetType AssetType { get; set; } = new AssetType();
        public string Manufacturer { get; set; } = "";
        [Display(Name = "Numéro de série")]
        public string? SerialNumber { get; set; } = "";
        public string? Description { get; set; } = "";
        public Location? Location { get; set; } 
        public Collection<ExtendedPropertyValue> PropertiesValues { get; set; } = new Collection<ExtendedPropertyValue>();
        
    }
}