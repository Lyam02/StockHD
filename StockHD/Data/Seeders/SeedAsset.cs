
using StockHD.Models;
using System.Collections.ObjectModel;

namespace StockHD.Data.Seeders
{
    public static class SeedAsset
    {
        public static void Seed(StockDbContext context)
        {

            if (!context.Assets.Any())
            {
                context.Assets.AddRange(

                    new Asset() {Manufacturer = "HP", SerialNumber = "6GFVSRY9CN", Description = "",AssetTypes = new ObservableCollection<AssetType>(
                        context.Types.Where(a => a.Name == "PC")),
                        PorpertiesValues = new ObservableCollection<ExtendedPropertyValue>(context.PropertiesValues.Where(p => p.Value == ""))}

                );
                
                context.SaveChanges();
            }
        }
    }
}