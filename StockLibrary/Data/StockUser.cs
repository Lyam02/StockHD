using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StockLibrary.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    }
}
