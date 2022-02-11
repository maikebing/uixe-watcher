using DevExpress.LookAndFeel;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Extensions;

namespace Uixe.Watcher
{
    public static class Program
    {

        public static void Main(string[] args)
        {
           
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration( config=>
                    {
                        config.AddJsonFile(System.IO.Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.MyDocuments), $"appsettings.User.json"),true,true);
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseWindowsFormsLifetime<frmMain>();
                    webBuilder.UseUrls("http://0.0.0.0:9999/");
                });

   
    }
}