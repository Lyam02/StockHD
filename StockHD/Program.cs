

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using StockHD.Data;
using StockHD.Data.Seeders;

namespace StockHD
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var app = CreateHostBuilder(args).Build();


            // Add services to the container.
           // builder.Services.AddControllersWithViews();

           //var app = builder.Build();



            // Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {

                    using (var serviceScope = services.GetService<IServiceScopeFactory>().CreateScope())
                    {
                        var context = services.GetRequiredService<StockDbContext>();

                       // context.Database.Migrate();

                        


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


    }
}