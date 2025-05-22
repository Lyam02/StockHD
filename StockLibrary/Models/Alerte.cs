using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary.Models
{
    public class AlertAsset
    {
        public string AssetTypeName { get; set; }  // Le nom du type d'asset
        public int AssetCount { get; set; }        // Le nombre total d'assets de ce type
        public int StockAssetCount { get; set; }   // Le nombre d'assets de ce type dans "Stock"
    }


}
