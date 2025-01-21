
using StockHD.Models;
using System.Collections.ObjectModel;

namespace StockHD.Data.Seeders
{
    public static class SeedAsset
    {
        public static void Seed(StockDbContext context)
        {

           /* if (!context.Assets.Any())
            {
                context.Assets.AddRange(

                    new Asset() {
                        Manufacturer = "HP",
                        SerialNumber = "6GFVSRY9CN", 
                        Description = "",
                        AssetType = context.Types.FirstOrDefault(a => a.Name == "PC")!,
                        PropertiesValues = new ObservableCollection<ExtendedPropertyValue>(context.PropertiesValues.Where(p => p.Value == "")), 
                        Location = context.Locations.FirstOrDefault(l => l.Name == "Stock" )!}
                );
                
                context.SaveChanges();
            }*/
        }
    }
}