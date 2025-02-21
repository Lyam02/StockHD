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
    public class StockUserRole : IdentityUserRole<String>
    {
        public virtual StockUser? User { get; set; }
        public virtual StockRole? Role { get; set; }
    }

    public class StockRole : IdentityRole
    {
        public virtual ICollection<StockUserRole> Users { get; } = new List<StockUserRole>();
    }
}
