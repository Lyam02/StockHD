using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using StockLibrary;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace StockHD_Lourd
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            ServiceProvider = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Configuration de la connexion à la base de données
         //   services.AddDbContext<StockDbContext>(options =>
           //     options.UseMySql("Server=MON_SERVEUR;Database=MA_BASE;User Id=MON_USER;Password=MON_MDP;"));


            IConfigurationRoot Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();



            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
            bool UseSqLite = Configuration.GetValue<bool>("UseSqLite");

            if (UseSqLite)
            {
                optionsBuilder.UseSqlite(Configuration.GetConnectionString("SqlLiteDbContext"));
                services.AddDbContext<StockDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqlLiteDbContext")));
            }
            else
            {
                optionsBuilder.UseMySql(
                                    Configuration.GetConnectionString("MySqlDbContext")
                                    , new MySqlServerVersion(Configuration.GetValue<string>("MySqlVersion"))
                                    );
                services.AddDbContext<StockDbContext>(options => options.UseMySql(
                                    Configuration.GetConnectionString("MySqlDbContext")
                                    , new MySqlServerVersion(Configuration.GetValue<string>("MySqlVersion"))
                                    ));
            }







            return services.BuildServiceProvider();
        }
    }
}
