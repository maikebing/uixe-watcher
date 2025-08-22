using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.AspNetCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Uixe.Watcher.Extensions;
using Utf8StringInterpolation;
using ZLogger;
namespace Uixe.Watcher
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
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            services.Configure<AppSettings>(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Uixe.WatcherPlus", Version = "v1" });
            });
            services.AddForms();
            services.AddMemoryCache();
            services.AddLogging(configure =>
            {
                var logdir = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "uixe", $"uixe{DateTime.Now:yyyyMMddhhmm}.log"));
                if (!logdir.Directory.Exists) logdir.Directory.Create();
                configure.AddZLoggerFile(logdir.FullName, cfg =>
                {
                    cfg.UsePlainTextFormatter(formatter =>
                    {
                        formatter.SetPrefixFormatter($"{0}|{1}|", (in MessageTemplate template, in LogInfo info) => template.Format(info.Timestamp, info.LogLevel));
                        formatter.SetSuffixFormatter($" ({0})", (in MessageTemplate template, in LogInfo info) => template.Format(info.Category));
                        formatter.SetExceptionFormatter((writer, ex) => Utf8String.Format(writer, $"{ex.Message}"));
                    }
                            );
                });
            });
            var cfg = new System.IO.FileInfo(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Uixe", "data.db"));
            if (!cfg.Directory.Exists) cfg.Directory.Create();
            services.AddSingleton(new LiteDB.ConnectionString() { Filename = cfg.FullName, Connection = LiteDB.ConnectionType.Shared, Password = "kissme", Upgrade = true });

            services.AddQuartz(q =>
            {
                q.DiscoverJobs();
            });
            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                options.StartDelay = TimeSpan.FromSeconds(10);
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

        }

 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uixe.WatcherPlus v1"));
                app.UseDebugLane("10.37.47.136", "10.37.48.135");
            }
       
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
