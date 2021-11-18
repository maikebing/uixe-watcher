using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddForms(this IServiceCollection services, Assembly assembly = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            var formType = typeof(Form);
            var formImplementationTypes = assembly.GetTypes()
                .Where(x => formType.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .ToArray();

            foreach (var formImplementationType in formImplementationTypes)
            {
                var descriptor = new ServiceDescriptor(formImplementationType, formImplementationType, lifetime);
                services.Add(descriptor);
            }

            return services;
        }
    }
    // An attempt to mimic the structure and functionality of the official ConsoleLifetime
    // https://github.com/dotnet/extensions/blob/master/src/Hosting/Hosting/src/Internal/ConsoleLifetime.cs

    public class WindowsFormsLifetime : IHostLifetime, IDisposable
    {
        public WindowsFormsLifetimeOptions Options { get; }
        public IHostEnvironment Environment { get; }
        public IHostApplicationLifetime ApplicationLifetime { get; }
        public HostOptions HostOptions { get; }
        private ILogger Logger { get; }

        private readonly ManualResetEvent _shutdownBlock;

        private CancellationTokenRegistration _applicationStartedRegistration;
        private CancellationTokenRegistration _applicationStoppingRegistration;

        public WindowsFormsLifetime(
            IOptions<WindowsFormsLifetimeOptions> options,
            IHostEnvironment environment,
            IHostApplicationLifetime applicationLifetime,
            IOptions<HostOptions> hostOptions,
            ILoggerFactory loggerFactory)
        {
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            HostOptions = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
            Logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");

            _shutdownBlock = new ManualResetEvent(false);
            _applicationStartedRegistration = default;
            _applicationStoppingRegistration = default;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            if (!Options.SuppressStatusMessages)
            {
                _applicationStartedRegistration = ApplicationLifetime.ApplicationStarted.Register(state =>
                {
                    ((WindowsFormsLifetime)state).OnApplicationStarted();
                },
                this);

                _applicationStoppingRegistration = ApplicationLifetime.ApplicationStopping.Register(state =>
                {
                    ((WindowsFormsLifetime)state).OnApplicationStopping();
                },
                this);
            }

            Application.ApplicationExit += OnExit;

            return Task.CompletedTask;
        }

        private void OnApplicationStarted()
        {
            Logger.LogInformation("Application started. Close the main form to shut down.");
            Logger.LogInformation("Hosting environment: {envName}", Environment.EnvironmentName);
            Logger.LogInformation("Content root path: {contentRoot}", Environment.ContentRootPath);
        }

        private void OnApplicationStopping()
        {
            Logger.LogInformation("Application is shutting down...");
        }

        private void OnExit(object sender, EventArgs e)
        {
            ApplicationLifetime.StopApplication();

            if (!_shutdownBlock.WaitOne(HostOptions.ShutdownTimeout))
            {
                Logger.LogInformation("Waiting for the host to be disposed. Ensure all 'IHost' instances are wrapped in 'using' blocks.");
            }

            _shutdownBlock.WaitOne();

            System.Environment.ExitCode = 0;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _shutdownBlock.Set();

            Application.ApplicationExit -= OnExit;

            _applicationStartedRegistration.Dispose();
            _applicationStoppingRegistration.Dispose();
        }
    }
}
