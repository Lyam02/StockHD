using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary.Models
{
    public class AccessoireAssignement
    {
        [Display(Name = "Corporate Key")]
        public string? CorpUserCK { get; set; }
        public string? AccessoireName {  get; set; }
        public int Quantite { get; set; }
        public CorpUser CorpUser { get; set; }
        public Accessoire Accessoire { get; set; }
    }
}
