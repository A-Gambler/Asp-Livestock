using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using livestock.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using LiveStok.Helpers;

namespace livestock
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                  options.EnableSensitiveDataLogging(true)
                  .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(20);//You can set Time   
            });

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddMvc(o =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    o.Filters.Add(new AuthorizeFilter(policy));
            //})
            // .AddRazorPagesOptions(options =>
            // {
            //     //options.Conventions.AuthorizePage("/Contact");
            //     //options.Conventions.AuthorizeFolder("/Private");
            //     options.Conventions.AllowAnonymousToPage("/Index");
            //     //options.Conventions.AllowAnonymousToFolder("/Private/PublicPages");
            // })
            //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            var supportedCultures = new[]
            {
                //new System.Globalization.CultureInfo("en"),
                //new System.Globalization.CultureInfo("en-US"),
                new System.Globalization.CultureInfo("en-AU"),
                //new System.Globalization.CultureInfo("es"),
                //new System.Globalization.CultureInfo("es-ES")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-AU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //app.UseMvc();

            app.UseMvc(routes =>
             {
                                 routes.MapRoute(
                                     name: "default",
                                     template: "{controller=Home}/{action=Index}/{id?}");
                             });
            

        }
    }
}
