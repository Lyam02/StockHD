using StockLibrary.Models;
using System.Collections.ObjectModel;

namespace StockLibrary.Data.Seeders
{
    public static class SeedAsset
    {
        public static void Seed(StockDbContext context)
        {

            /*if (!context.Assets.Any())
            {
                context.Assets.AddRange(

                    new Asset()
                    {
                        Manufacturer = "HP",
                        
                        Description = "",
                        AssetType = context.Types.FirstOrDefault(a => a.Name == "PC")!,
                        PropertiesValues = new ObservableCollection<ExtendedPropertyValue>(context.PropertiesValues.Where(p => p.Value == "")),
                        Location = context.Locations.FirstOrDefault(l => l.Name == "Stock")!
                    }
                );

                context.SaveChanges();
            }*/
        }
    }
}