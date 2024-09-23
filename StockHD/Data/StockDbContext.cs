using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



using Microsoft.EntityFrameworkCore;
using StockHD.Data.Seeders;
using StockHD.Models;


//test git


namespace StockHD.Data
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





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>().ToTable("Assets").HasMany(e=> e.PorpertiesValues);
            modelBuilder.Entity<Location>().ToTable("Locations");
            modelBuilder.Entity<AssetType>().ToTable("AssetType").HasMany(e => e.Properties).WithMany(e=>e.AssetTypes);
            modelBuilder.Entity<ExtendedProperty>().ToTable("Properties");
            modelBuilder.Entity<ExtendedPropertyValue>().ToTable("PropertiesValues");

        }

        }

    }
