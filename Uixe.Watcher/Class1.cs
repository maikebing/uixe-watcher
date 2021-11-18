using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uixe.Watcher
{
    public class WindowsFormsApplicationOptions
    {
#if NETCOREAPP

        public HighDpiMode HighDpiMode { get; set; }
#endif
        public bool EnableVisualStyles { get; set; }
        public bool CompatibleTextRenderingDefault { get; set; }

        public WindowsFormsApplicationOptions()
        {
#if NETCOREAPP
            HighDpiMode = HighDpiMode.SystemAware;
#endif
            EnableVisualStyles = true;
            CompatibleTextRenderingDefault = false;
        }
    }
    public class WindowsFormsLifetimeOptions
    {
        /// <summary>
        /// Gets or sets a value that indicates if host lifetime status messages should be supressed (such as on startup).
        /// The default is false.
        /// </summary>
        public bool SuppressStatusMessages { get; set; }
    }
    public class WindowsFormsApplicationHostedService : IHostedService
    {
        private readonly WindowsFormsApplicationOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public WindowsFormsApplicationHostedService(IOptions<WindowsFormsApplicationOptions> options, IServiceProvider serviceProvider)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var thread = new Thread(UIThreadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            return Task.CompletedTask;
        }

        private void UIThreadStart()
        {
#if NETCOREAPP
            Application.SetHighDpiMode(_options.HighDpiMode);
#endif

            if (_options.EnableVisualStyles)
            {
                Application.EnableVisualStyles();
            }

            Application.SetCompatibleTextRenderingDefault(_options.CompatibleTextRenderingDefault);

            var applicationContext = _serviceProvider.GetRequiredService<ApplicationContext>();

            Application.Run(applicationContext);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
