using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Uixe.Watcher.Extensions;

namespace Uixe.Watcher
{
    public class WindowsFormsApplicationHostedService : IHostedService
    {
        private readonly WindowsFormsApplicationOptions _options;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment env;

        public WindowsFormsApplicationHostedService(IOptions<WindowsFormsApplicationOptions> options, IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.env = env;
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
