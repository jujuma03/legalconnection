using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using LC.CORE.Services.Implementations;
using LC.CORE.Services.Interfaces;
using LC.CORE.Services.Models;
using LC.CORE.VIEW.Services.Implementations;
using LC.CORE.VIEW.Services.Interfaces;
using LC.DATABASE.Data;
using LC.HANGFIRE.Filters;
using LC.HANGFIRE.Services.DelayedTask.Implementations;
using LC.HANGFIRE.Services.DelayedTask.Interfaces;
using LC.HANGFIRE.Services.RecurringTask.Implementations;
using LC.HANGFIRE.Services.RecurringTask.Interfaces;
using LC.HANGFIRE.Services.Startup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LC.HANGFIRE
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
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddDbContext<LegalConnectionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), options => options.EnableRetryOnFailure()));

            services.Configure<EmailConfig>(Configuration.GetSection("Email"));

            #region Services
            //Recurring Task Services
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IViewRenderService, ViewRenderService>();

            services.AddScoped<IRecurringTaskLawyerService, RecurringTaskLawyerService>();
            services.AddScoped<IDelayedTaskLegalCaseService, DelayedTaskLegalCaseService>();

            #endregion

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseHangfireDashboard();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                Authorization = new[] { new AuthorizationFilter() }
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard("/jobs");
            });

            recurringJobManager.InjectRecurringJobs();

        }
    }
}
