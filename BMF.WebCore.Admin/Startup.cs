using BMF.Shared.Core;
using BMF.Shared.Services.Authentication;
using BMF.Shared.Services.Data;
using BMF.Shared.Services.Menu;
using BMF.Shared.Services.Security;
using BMF.Shared.UI;
using BMF.WebCore.Admin.Data;
using BMF.WebCore.Admin.Services.Menu;
using BMF.WebCore.Admin.Services.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;

namespace BMF.WebCore.Admin
{
    public class Startup
    {
        #region Fields

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        #endregion

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //most of API providers require TLS 1.2 nowadays
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //add accessor to HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Database
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<BMFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDbContextProd")));
            }
            else if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Staging")
            {
                services.AddDbContext<BMFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDbContextStaging")));
            }
            else
            {
                services.AddDbContext<BMFDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDbContextLocal")));
            }
            //services.BuildServiceProvider().GetService<KahwaDbContext>().Database.Migrate();

            // Dependency Injection
            // Services
            // ------ Security
            services.AddScoped<IAdminRoleService, AdminRoleService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<ISystemOperationService, SystemOperationService>();
            services.AddScoped<ISystemOperationsProvider, SystemOperationsProvider>();
            services.AddScoped<IPermissionService, PermissionService>();
       
            // ------ Send Grid

            // System Builder Services
            services.AddScoped<ISiteMapService, AdminSiteMapService>();
            services.AddScoped<IPageHeadBuilder, PageHeadBuilder>();
            services.AddScoped<IWorkContext, WebWorkContext>();
            services.AddScoped<IDataSeeder, EFCoreDataSeeder>();
            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            // Authentication + Dependency Injection
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = AuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = AuthenticationDefaults.AuthenticationScheme;
            });
            authenticationBuilder.AddCookie(AuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = $"{CookieDefaults.Prefix}{CookieDefaults.AuthenticationCookie}";
                options.Cookie.HttpOnly = true;
                options.LoginPath = AuthenticationDefaults.LoginPath_Admin;
                options.AccessDeniedPath = AuthenticationDefaults.AccessDeniedPath;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.AddScoped<IAuthenticationService, CookieAuthenticationService>();

            // MVC
            services.AddControllersWithViews()
                .AddNewtonsoftJson(x =>
                {
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<AuthenticationMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                foreach (var SingleArea in AreaNames.All)
                    endpoints.MapAreaControllerRoute(
                        name: SingleArea,
                        areaName: SingleArea,
                        pattern: SingleArea + "/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
