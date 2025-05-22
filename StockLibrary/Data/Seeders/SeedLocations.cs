
using StockLibrary.Models;
using System.Collections.ObjectModel;

namespace StockLibrary.Data.Seeders
{
    public static class SeedLocations
    {
        public static void Seed(StockDbContext context)
        {


            if (!context.Locations.Any())
            {
                 context.Locations.AddRange(
                     new Location
                     () { 
                         Name="Stock", 
                         Description="Stock Matériel", 
                         Code="stck",
                         Assets = new Collection<Asset> (context.Assets.Where(a => a.AssetType.Name == "PC").ToList()!)
                     }

                     ,new Location() { Name = "HelpDesk", Description = "Bureau HelpDesk", Code = "hlp" }

                     , new Location() { Name = "Réunion 4.001", Description = "Salle de réunion 4eme", Code = "04.001" }

                     , new Location()
                     {
                         Name = "Réunion 4.002",
                         Description = "Salle de réunion 4eme",
                         Code = "04.002"
                         
                     }
                     );
                context.SaveChanges();
            }
        }
    }
}
