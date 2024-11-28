using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockHD.Models
{
    public class Location
    {
        public int Id { get; set; }
        [Display(Name = "Localisation")]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Collection<Asset> Assets { get; set; } = new Collection<Asset>();

    }
}
