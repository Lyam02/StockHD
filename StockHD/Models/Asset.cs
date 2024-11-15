using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class Asset
    {
        public int Id { get; set; }
        //public string Name { get; set; } = "";
        //[Display(AssetTypes = "Types" )]
        public AssetType AssetType { get; set; } = new AssetType();
        public string Manufacturer { get; set; } = "";
        public string? SerialNumber { get; set; } = "";
        public string? Description { get; set; } = "";
        public Location Location { get; set; } = new Location();
        public Collection<ExtendedPropertyValue> PropertiesValues { get; set; } = new Collection<ExtendedPropertyValue>();
    }
}