using Microsoft.Extensions.DependencyInjection;
using System;

namespace Uixe.Watcher
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class ServiceLifetimeAttribute : Attribute
    {
        // This is a positional argument
        public ServiceLifetimeAttribute(ServiceLifetime lifetime)
        {

            Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }
    }
}
