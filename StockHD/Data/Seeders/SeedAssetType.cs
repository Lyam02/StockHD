
using StockHD.Models;
using System.Collections.ObjectModel;

namespace StockHD.Data.Seeders
{
    public static class SeedAssetType
    {
        public static void Seed(StockDbContext context)
        {


            if(!context.Properties.Any())
            {
                context.Properties.AddRange(
                        new ExtendedProperty() { Name="Résolution"}
                        , new ExtendedProperty() { Name="Taille"}
                        , new ExtendedProperty() { Name="Modele"}
                    );
                context.SaveChanges();
            }


            if (!context.Types.Any())
            {
                 context.Types.AddRange(
                     new AssetType() {  Name="Ecran"
                                        , Description=""
                                        , Properties= new ObservableCollection<ExtendedProperty>(
                                            context.Properties.Where(p=>p.Name=="Taille" || p.Name == "Résolution")
                                            ) }
                     ,new AssetType() { Name = "Télé",Description = "",
                                             Properties = new ObservableCollection<ExtendedProperty>(
                                            context.Properties.Where(p => p.Name == "Taille")
                                            )
                     }


                     , new AssetType() { Name = "Clavier", Description = "" }
                     , new AssetType() { Name = "Souris", Description = "" }
                     , new AssetType() { Name = "Dock", Description = "" }
                     , new AssetType() { Name = "PC", Description = "", Properties = new ObservableCollection<ExtendedProperty>(
                                                                        context.Properties.Where(p => p.Name == "Modele"))}
                     );
                context.SaveChanges();
            }


        }
    }
}
