using AKDEMIC.LC.WEB.Profiles;
using AutoMapper;
using LC.CORE.Services.Models;
using LC.DATABASE.Data;
using LC.DATABASE.Data.Seeds;
using LC.ENTITIES.Models;
using LC.PAYMENT.Models;
using LC.WEB.Extensions;
using LC.WEB.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB
{
    public class Startup
    {
        private readonly IWebHostEnvironment CurrentEnvironment;
        private readonly string CulquiOrigins = "_culquiOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            CurrentEnvironment = webHostEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region DATABASE

            services.AddDbContext<LegalConnectionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),options => options.EnableRetryOnFailure()));

            #endregion

            #region IDENTITY

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
               .AddEntityFrameworkStores<LegalConnectionContext>()
               .AddDefaultTokenProviders();

            #endregion

            #region REPOSITORY / SERVICE
            services.AddRepository();
            services.AddTransientServices();
            services.AddMemoryCache();
            services.AddSignalR();
            #endregion

            #region CONFIGURATION

            services.AddCors(options =>
            {
                options.AddPolicy(name: CulquiOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://api.culqi.com");
                    });
            });
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            services.Configure<CloudStorageCredentials>(Configuration.GetSection("CloudStorageCredentials"));
            services.Configure<PaymentCredentials>(Configuration.GetSection("OnlinePayment"));

            #endregion

            #region Auth
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                 .AddCookie(options =>
                 {
                     options.Cookie.IsEssential = true;
                     //options.Cookie.SameSite = SameSiteMode.None;
                 })
                 .AddGoogle(options =>
                 {
                     IConfigurationSection AuthSection =
                         Configuration.GetSection("Authentication:Google");

                     options.ClientId = AuthSection["ClientId"];
                     options.ClientSecret = AuthSection["ClientSecret"];
                 });
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            #endregion

            #region Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";
                options.Cookie.Name = "LCAUTH";
            });

            if (!CurrentEnvironment.IsDevelopment())
            {
                services.AddControllersWithViews();
            }
            else
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();
            }

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var forwardedHeadersOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };
            forwardedHeadersOptions.KnownNetworks.Clear();
            forwardedHeadersOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardedHeadersOptions);

            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use(async (ctx, next) =>
                {
                    await next();
                    if(ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
                    {
                        //Re-execute the request so the user gets the error page
                        string originalPath = ctx.Request.Path.Value;
                        ctx.Items["originalPath"] = originalPath;
                        ctx.Request.Path = "/error/404";
                        await next();
                    }
                });

                app.UseExceptionHandler("/error/500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(CulquiOrigins);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Portal}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Portal}/{action=Index}/{id?}");

                endpoints.MapHub<LegalConnectionHub>("/LegalConnectionHub");
            });
        }
    }
}
