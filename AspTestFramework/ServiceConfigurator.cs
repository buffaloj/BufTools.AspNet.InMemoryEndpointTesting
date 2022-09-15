using AspTestFramework.Exceptions;
using AspTestFramework.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspTestFramework
{
    internal class ServiceConfigurator : IServiceConfigurator
    {
        private readonly IServiceCollection _services;

        public ServiceConfigurator(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceConfigurator UseDependency<T>(T instance)
            where T : class
        {
            var descriptorToRemove = _services.FirstOrDefault(d => d.ServiceType == typeof(T));
            if (descriptorToRemove == null)
            {
                throw new DependencyNotRegisteredException(string.Format(FrameworkResources.DependencyNotRegisteredFormat, typeof(T).FullName));
            }
            _services.Remove(descriptorToRemove);

            switch (descriptorToRemove?.Lifetime)
            {
                case ServiceLifetime.Singleton:
                    _services.AddSingleton(serviceProvider => instance);
                    break;
                case ServiceLifetime.Scoped:
                    _services.AddScoped(serviceProvider => instance);
                    break;
                case ServiceLifetime.Transient:
                    _services.AddTransient(serviceProvider => instance);
                    break;
                default:
                    throw new NotImplementedException($"Unknown Service Lifetime: {descriptorToRemove?.Lifetime}");
            }

            return this;
        }
    }
}
