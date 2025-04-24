using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StockLibrary.Data
{
    public class StockUser : IdentityUser
    {
        [PersonalData]
        [Display(Name = "Prénom")]
        public string Surname { get; set; }

        [PersonalData]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [PersonalData]
        [Display(Name = "Phrase secrète")]
        public string SecretSentense { get; set; }

        public virtual ICollection<StockUserRole> Roles { get; } = new List<StockUserRole>();
    }
}
