using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockLibrary.Models
{
    public class Assignation
    {
        public string AssetId { get; set; }
        public string CorpUserCK { get; set; }

        [Display(Name = "Date d'assignation")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Asset Asset { get; set; }
        public CorpUser CorpUser { get; set; }

    }
}
