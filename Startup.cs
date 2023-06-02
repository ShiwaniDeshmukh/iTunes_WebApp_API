using iTunes_WebApp_API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using iTunes_WebApp_API.Models;
using iTunes_WebApp_API.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using iTunes_WebApp_API.Models.Repositories;


namespace iTunes_WebApp_API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            services.AddControllers();
            services.AddHttpClient();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "MyAppAuthCookie";
                    options.LoginPath = "/Authentication/SignIn";
                });

            services.AddScoped<IUserRepository, UserRepository>();
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "signup",
                    pattern: "Authentication/{action=SignUp}/{id?}",
                    defaults: new { controller = "Authentication" });


                endpoints.MapControllerRoute(
                    name: "itunes",
                    pattern: "iTunes/{action=Index}/{id?}",
                    defaults: new { controller = "iTunesStore" });

                endpoints.MapControllerRoute(
                    name: "home",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllers();
            });


        }
    }
}