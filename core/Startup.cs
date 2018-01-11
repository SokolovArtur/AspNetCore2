using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tochka.Areas.Accounts.Data;
using Tochka.Areas.Geodata.Services;
using Tochka.Areas.Hr.Services;
using Tochka.Data;
using Tochka.Services;

namespace Tochka
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Auth/Login";
                options.LogoutPath = "/Accounts/Auth/Logout";
            });
            services.Configure<SecurityStampValidatorOptions>(
                options => { options.ValidationInterval = TimeSpan.FromMinutes(5); });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<ICityRepository, EFCityRepository>();
            services.AddTransient<IVacancyRepository, EFVacancyRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}