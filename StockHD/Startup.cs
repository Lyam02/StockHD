using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockLibrary.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Net;
using StockLibrary.Models.Auth;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.Extensions.DependencyInjection;
using StockLibrary;
using StockLibrary.Data.Seeders;

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


            var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();

            bool UseSqLite = Configuration.GetValue<bool>("UseSqLite");

            if(UseSqLite)
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



            var context = new StockDbContext(optionsBuilder.Options);

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddIdentity<StockUser, StockRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddUserStore<UserStore<StockUser, StockRole, StockDbContext, string,
                                IdentityUserClaim<string>, StockUserRole, IdentityUserLogin<string>,
                                IdentityUserToken<string>, IdentityRoleClaim<string>>>()
                .AddRoleStore<RoleStore<StockRole, StockDbContext, string, StockUserRole, IdentityRoleClaim<string>>>()
                .AddRoles<StockRole>()
                .AddEntityFrameworkStores<StockDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;
            });


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
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
            
            //SeedIdentity.SeedIdentityRoleUser(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                   // pattern: "{controller=Home}/{action=Index}/{id?}"
                    pattern: "{controller=Auth}/{action=SignInUser}/{id?}"
                    );
              //  endpoints.MapRazorPages();

            });



        }
    }
}
