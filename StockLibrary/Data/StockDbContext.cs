﻿using System;
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
        public StockDbContext()
        {
        }

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
        public DbSet<AlertAsset> AlertAssets { get; set; }
        public DbSet<Stat> Stat { get; set; }
        public DbSet<Accessoire> Accessoire { get; set; }
        public DbSet<AccessoireAssignement> AccessoireAssignement { get; set; }


        // Identity
        public DbSet<StockRole> Roles { get; set; }
        public DbSet<StockUser> Users { get; set; }
        public DbSet<StockUserRole> UserRoles {get; set;}
        public DbSet<IdentityUserClaim<string>> UserClaims{ get; set; }
        public DbSet<IdentityRoleClaim<string>> RoleClaims{ get; set; }

        //public DbSet<IdentityUserAsset> UserAssets { get; set; }


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
            modelBuilder.Entity<AlertAsset>().ToTable("AlertAsset").HasNoKey();
            modelBuilder.Entity<Stat>().ToView("Stat").HasKey(s => new {s.LocationName, s.AssetTypes});
            modelBuilder.Entity<Accessoire>().ToTable("Accessoire").HasKey(a => a.Name);
            modelBuilder.Entity<AccessoireAssignement>().ToTable("AccessoireAssignement").HasKey(a => new { a.CorpUserCK, a.AccessoireName });

            // Identity
            modelBuilder.Entity<StockRole>().ToTable("Role").HasKey(r => r.Id);
            modelBuilder.Entity<StockUser>().ToTable("User").HasKey(u => u.Id);
            modelBuilder.Entity<StockUserRole>().ToTable("UserRole").HasKey(r => new {r.UserId, r.RoleId});
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

        }
    }
}