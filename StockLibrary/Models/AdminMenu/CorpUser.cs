
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace StockLibrary.Models
{
    public class CorpUser
    {
        [Required]
        [Display(Name = "Corporate Key" )]
        public string CK { get; set; }
        [Required]
        [Display(Name = "Prénom")]
        public string Surname { get; set; }
        [Required]
        [Display (Name = "Nom")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "Adresse Mail")]
        public string EmailAddress { get; set; }
        [Display(Name = "Service")]
        public string Departement { get; set; }
        [Display(Name = "Date d'effet")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string? StartDate { get; set; }
        [Display(Name = "Matériels")]
        public Collection<Asset>? Assets { get; set; } = new Collection<Asset>();
    }
}