using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class AssetType
    {
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Collection<Asset> Assets { get; set; } = new Collection<Asset>();
        public Collection<ExtendedProperty> Properties { get; set; }= new Collection<ExtendedProperty>();
    }

    public class ExtendedProperty
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Collection<AssetType> AssetTypes { get; set; } = new Collection<AssetType>();
    }

    public class ExtendedPropertyValue
    {
        public int Id { get; set; }
 

        public ExtendedProperty Property { get; set; } = new ExtendedProperty();
        public Asset Asset { get; set; } = new Asset();
        public string Value { get; set; } = string.Empty;
       
    }
}