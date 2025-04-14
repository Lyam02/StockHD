using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary.Models
{
    public class Accessoire
    {
        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Quantité")]
        public int Quantite { get; set; }
    }
}
