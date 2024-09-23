using System.Collections.ObjectModel;

namespace StockHD.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Collection<Asset> Assets { get; set; } = new Collection<Asset>();

    }
}
