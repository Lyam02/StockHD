using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary.Models
{
    public class Stat
    {
        [Display(Name = "Utilisateur")]
        public string LocationName { get; set; }

        [Display(Name = "Matériel")]
        public string AssetTypes { get; set; }

        [Display(Name = "Nombre")]
        public int NBMatriel { get; set; }
    }
}
