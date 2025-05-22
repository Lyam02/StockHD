

using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using StockHD.Data;
=======
using Microsoft.Extensions.Hosting;
using StockLibrary.Data;
using StockLibrary.Data.Seeders;
using Microsoft.AspNetCore.Identity;
using StockLibrary;
>>>>>>> Dev

namespace StockHD
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            Console.WriteLine("debug: "+config.GetConnectionString("SqlLiteDbContext"));

#if DEBUG
            var app = CreateHostBuilder(args).Build();
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("fr-FR");

<<<<<<< HEAD
#else
            var app = (config.GetValue<bool>("SelfHosting")) ? CreateHostBuilderSelf(args).Build() : CreateHostBuilder(args).Build();
#endif

            // Add services to the container.
            // builder.Services.AddControllersWithViews();

            //var app = builder.Build();



=======
            // _________________________________________________________________________________________________________________________________________________
            // Add services to the container.
            // builder.Services.AddControllersWithViews();
            //var app = builder.Build();
>>>>>>> Dev
            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            // _________________________________________________________________________________________________________________________________________________


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    using (var serviceScope = services.GetService<IServiceScopeFactory>().CreateScope())
                    {
                        var context = services.GetRequiredService<StockDbContext>();
                        var userManager = (UserManager<IdentityUser>)serviceScope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
                        
                        context.Database.Migrate();

                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });




        public static IHostBuilder CreateHostBuilderSelf(string[] args) =>
         Host.CreateDefaultBuilder(args)
             //.UseSystemd()
             //.UseWindowsService()
             //.UseContentRoot(Directory.GetCurrentDirectory())

             .ConfigureWebHostDefaults(webBuilder =>
             {

                //webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                // webBuilder.UseIISIntegration();
                 webBuilder.UseKestrel(options =>
                 {
                     // HTTP 5000
                     options.ListenLocalhost(5000);

                     // HTTPS 5001
                     options.ListenLocalhost(5001, builder =>
                     {
                         builder.UseHttps();
                     });
                 });
                 webBuilder.UseStartup<Startup>(); ;
             });

        

    }
}