

namespace StockLibrary.Data.Seeders
{
    public class SeedData
    {
        public static void StartupData(StockDbContext context)
        {

            SeedLocations.Seed(context);
            SeedAssetType.Seed(context);
            SeedAsset.Seed(context);
            
        }

    }
}
