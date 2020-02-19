using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using AybCommerce.Core.Application.Services;
using AybCommerce.Core.Interfaces.Services;
using AybCommerce.Domain.Entities;
using AybCommerce.Persistance.Data;
using AybCommerce.UI.Middlewares;
using AybCommerce.UI.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;

namespace AybCommerce.UI
{
    public class Startup
    {
        private readonly IServiceCollection _services;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureInMemoryDatabases(services);

            // use real database
            // ConfigureProductionServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<AybCommerceDbContext>(c => c.UseInMemoryDatabase("AybCommerce"));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<AybCommerceDbContext>()
                .AddDefaultTokenProviders();

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<AybCommerceDbContext>(c => c.UseSqlServer(
                Configuration.GetConnectionString("AybCommerceConnection")
                , sqlOptions => sqlOptions.MigrationsAssembly("AybCommerce.Persistance.Data")));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AybCommerceDbContext>()
            .AddDefaultTokenProviders();

            ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            AddSmtpMailService(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICatalogService, CatalogService>();
            services.AddScoped<ICartItemService, CartItemService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddEntityFrameworkSqlServer();
            services.AddSingleton<LocalizationService>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // response compression with gzip
            AddResponseCompression(services);
            services.AddMemoryCache();
            services.AddSession();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("AybCommerceConnection"));
                app.UseDeveloperExceptionPage();
                ListAllRegisteredServices(app);
                app.UseDatabaseErrorPage();
            }
            else
            {
                NLogConfiguration(app);
                //app.UseExceptionHandler("/Home/Error/{0}");
                app.UseHsts();
            }

            // gzip compression
            app.UseResponseCompression();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            AppUseLocalization(app);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void ListAllRegisteredServices(IApplicationBuilder app)
        {
            app.Map("/allservices", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.WriteAsync(sb.ToString());
            }));
        }

        #region [ Configure Helpers ]

        private void NLogConfiguration(IApplicationBuilder app)
        {
            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("AybCommerceConnection"));
            var logFactory = NLogBuilder.ConfigureNLog("NLog.config");
            var logger = logFactory.GetCurrentClassLogger();
            app.ConfigureExceptionHandler(logger);
        }

        private static void AppUseLocalization(IApplicationBuilder app)
        {
            var englishRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
            var turkishRequestCulture = new RequestCulture(culture: "tr-TR", uiCulture: "tr-TR");

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("tr-TR")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = englishRequestCulture,
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            };

            var cookieProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First();
            var urlProvider = options.RequestCultureProviders.OfType<QueryStringRequestCultureProvider>().First();
            cookieProvider.Options.DefaultRequestCulture = englishRequestCulture;
            urlProvider.Options.DefaultRequestCulture = englishRequestCulture;
            cookieProvider.CookieName = CookieRequestCultureProvider.DefaultCookieName;

            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(cookieProvider);
            options.RequestCultureProviders.Add(urlProvider);
            app.UseRequestLocalization(options);
        }

        #endregion

        #region [ Configure Sevices Helpers ]

        private static void AddResponseCompression(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "imagejpeg" });
            });

            services.Configure<GzipCompressionProviderOptions>
            (options => options.Level =
                System.IO.Compression.CompressionLevel.Optimal);
        }

        private static void AddSmtpMailService(IServiceCollection services)
        {
            services.AddTransient((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<String>("MailConfiguration:Host"),
                    Port = config.GetValue<int>("MailConfiguration:Port"),
                    EnableSsl = true,
                    Credentials = new NetworkCredential(
                                            config.GetValue<String>("MailConfiguration:Username"),
                                            config.GetValue<String>("MailConfiguration:Password"))
                };
            });
        }

        #endregion
    }
}
