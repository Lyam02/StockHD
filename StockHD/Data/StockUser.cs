using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StockHD.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHD.Data
{
    public class StockUser : IdentityUser
    {
        [PersonalData]
        [Display(Name = "Prénom")]
        public string Surname { get; set; }

        [PersonalData]
        [Display(Name = "Nom")]
        public string Name { get; set; }

    }
}
