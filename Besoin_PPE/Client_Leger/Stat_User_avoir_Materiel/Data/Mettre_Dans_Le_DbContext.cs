// A mettre avec les autres DbSet :
public DbSet<Stat> Stat { get; set; }

// A mettre avec les autres modelBuilder :
modelBuilder.Entity<Stat>().ToView("Stat").HasKey(s => new {s.LocationName, s.AssetTypes});