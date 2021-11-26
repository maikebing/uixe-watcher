using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Windows.Forms;
using Uixe.Watcher.Extensions;

namespace Uixe.Watcher
{
    public static class HostBuilderExtensions
    {
        public static IWebHostBuilder UseWindowsFormsLifetime<TForm>(
            this IWebHostBuilder hostBuilder,
            Action<WindowsFormsApplicationOptions> configureApplication = null,
            Action<WindowsFormsLifetimeOptions> configureLifetime = null)
            where TForm : Form
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ApplicationContext>(c => new ApplicationContext(c.GetRequiredService<TForm>()));

                services.AddSingleton<IHostLifetime, WindowsFormsLifetime>();

                services.AddHostedService<WindowsFormsApplicationHostedService>();

                if (configureApplication != null)
                {
                    services.Configure(configureApplication);
                }

                if (configureLifetime != null)
                {
                    services.Configure(configureLifetime);
                }
            });
        }

        public static IWebHostBuilder UseWindowsFormsApplicationContextLifetime<TApplicationContext>(
            this IWebHostBuilder hostBuilder,
            Action<WindowsFormsApplicationOptions> configureApplication = null,
            Action<WindowsFormsLifetimeOptions> configureLifetime = null)
            where TApplicationContext : ApplicationContext
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ApplicationContext, TApplicationContext>();

                services.AddSingleton<IHostLifetime, WindowsFormsLifetime>();

                services.AddHostedService<WindowsFormsApplicationHostedService>();

                if (configureApplication != null)
                {
                    services.Configure(configureApplication);
                }

                if (configureLifetime != null)
                {
                    services.Configure(configureLifetime);
                }
            });
        }
    }
}
