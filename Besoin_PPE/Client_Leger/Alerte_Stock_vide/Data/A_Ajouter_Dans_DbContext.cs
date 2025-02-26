// A mettre avec les autre DbSet
public DbSet<AlertAsset> AlertAssets { get; set; }

//A mettre avec les autres modelBuilder
modelBuilder.Entity<AlertAsset>().ToTable("AlertAsset").HasNoKey();