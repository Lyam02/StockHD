using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockHD.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net;
using StockHD.Models.Auth;


namespace StockHD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddDbContext<StockDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqlLiteDbContext")));

            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("SqlLiteDbContext"));

            var context = new StockDbContext(optionsBuilder.Options);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddIdentity<StockUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StockDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseStatusCodePages(async contextAccessor =>
            {
                var response = contextAccessor.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                {
                    response.Redirect($"/Identity/Account/AccessDenied");
                }
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                   // pattern: "{controller=Home}/{action=Index}/{id?}"
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
              //  endpoints.MapRazorPages();

            });



        }
    }
}
