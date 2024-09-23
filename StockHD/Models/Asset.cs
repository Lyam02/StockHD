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
        public Collection<AssetType> AssetTypes { get; set; } = new Collection<AssetType>();
        public string Manufacturer { get; set; } = "";
        public string? SerialNumber { get; set; } = "";
        public string? Description { get; set; } = "";
        public Collection<Location> Location { get; set; } = new Collection<Location>();
        public Collection<ExtendedPropertyValue> PorpertiesValues { get; set; } = new Collection<ExtendedPropertyValue>();
    }
}