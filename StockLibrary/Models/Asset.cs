using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockLibrary.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public AssetType AssetType { get; set; } = new AssetType();
        [Display(Name = "Constructeur")]
        public string Manufacturer { get; set; } = "";
        public string? Description { get; set; } = "";
        public Location Location { get; set; } = new Location();
        [Display(Name = "Propriétés")]
        public Collection<ExtendedPropertyValue> PropertiesValues { get; set; } = new Collection<ExtendedPropertyValue>();
        [Display(Name = "Numéro de Série")]
        public SrNumber? SrNumber { get; set; }
    }
}