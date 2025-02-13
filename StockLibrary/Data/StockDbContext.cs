using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
//using NuGet.ContentModel;
using StockLibrary.Data.Seeders;
using StockLibrary.Models;
using StockLibrary.Data;
using StockLibrary.Models.Auth;
namespace StockLibrary
{

    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        { 
            this.Database.EnsureCreated();
            SeedData.StartupData(this);

        }
        
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AssetType> Types { get; set; }
        public DbSet<ExtendedProperty> Properties { get; set; }
        public DbSet<ExtendedPropertyValue> PropertiesValues { get; set; }
        public DbSet<SrNumber> SrNumber { get; set; }
        public DbSet<CorpUser> CorpUser { get; set; }


        // Identity
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<StockUser> Users { get; set; }
        public DbSet<IdentityUserRole<string>> UserRole {get; set;}
        public DbSet<IdentityUserClaim<string>> UserClaims{ get; set; }
        public DbSet<IdentityRoleClaim<string>> RoleClaims{ get; set; }

        // public DbSet<IdentityUserAsset> UserAssets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>().ToTable("Assets").HasMany(e=> e.PropertiesValues);
            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<AssetType>().ToTable("AssetType").HasMany(e => e.Properties).WithMany(e=>e.AssetTypes);
            modelBuilder.Entity<ExtendedProperty>().ToTable("Properties");
            modelBuilder.Entity<ExtendedPropertyValue>().ToTable("PropertiesValues");
            modelBuilder.Entity<SrNumber>().ToTable("SrNumber").HasKey(s=>s.SerialNumber);
            modelBuilder.Entity<CorpUser>().ToTable("CorpUser").HasKey(u=>u.CK);

            // Identity
            modelBuilder.Entity<IdentityRole>().ToTable("Role").HasKey(r => r.Id);
            modelBuilder.Entity<StockUser>().ToTable("User");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole").HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
        }
    }
}