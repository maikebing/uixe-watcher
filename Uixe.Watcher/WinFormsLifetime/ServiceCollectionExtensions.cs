using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Uixe.Watcher.Extensions;
using Uixe.Watcher.WinFormsLifetime;

namespace Uixe.Watcher
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddForm<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient) where T : Form
        {
            return services.AddForm(typeof(T), lifetime);
        }
        public static IServiceCollection AddForm(this IServiceCollection services, Type formImplementationType, ServiceLifetime lifetime = ServiceLifetime.Transient) 
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            if (!typeof(Form).IsAssignableFrom(formImplementationType))
            {
                throw new ArgumentNullException(nameof(formImplementationType));
            }
            var sla = formImplementationType.GetCustomAttribute<ServiceLifetimeAttribute>();
            if (sla != null)
            {
                lifetime = sla.Lifetime;
            }
            var descriptor = new ServiceDescriptor(formImplementationType, formImplementationType, lifetime);
            services.Add(descriptor);
            ConfigConsole();
            return services;
        }
        public static IServiceCollection AddForms(this IServiceCollection services, Assembly assembly = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }
            ConfigConsole();
            var formType = typeof(Form);
            var formImplementationTypes = assembly.GetTypes()
                .Where(x => formType.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .ToArray();

            foreach (var formImplementationType in formImplementationTypes)
            {
                var sla = formImplementationType.GetCustomAttribute<ServiceLifetimeAttribute>();
                if (sla != null)
                {
                    var descriptor = new ServiceDescriptor(formImplementationType, formImplementationType, sla.Lifetime);
                    services.Add(descriptor);
                }
            }
            return services;
        }
        private static void ConfigConsole()
        {
            //if (!_console)
            //{
            //    _console = true;
            //    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            //    {
            //        ConsoleWindow.Show();
            //    }
            //}
        }
    }
}
