using System;
using System.IO;
using Infrastructure.Configs;
using Infrastructure.LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Microsoft.AspNetCore.Identity;
using Models;
using Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Factories.Services;

namespace Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Get connection string from appsetting.json
            ModuleCommon.Configuration = Configuration;
            //----Register IUnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // ----Register Logger Service
            services.AddSingleton<ILoggerManager, LoggerManager>();
            //-----Register DbConnection
            services.AddDbContext<MyDbContext>(options => options.InitDbContext(Configuration), ServiceLifetime.Transient);
            //----Register DbContextIdentity
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();
            //----Add Authorization
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AspManager", policy =>
                {
                    policy.AddAuthenticationSchemes("Cookie", "Bearer");
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Admin");
                    policy.RequireClaim("IsAdmin", "true");
                });
            });
            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            //-----Register Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
            services.AddControllers();
            services.AddMemoryCache();
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.dev.config"));
                //do not attempt to use hot reloading on staging or production
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    EnvParam = new { NODE_ENV = "Development" }
                });
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Allow NLog and Sentry
                LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
                app.UseHsts();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
