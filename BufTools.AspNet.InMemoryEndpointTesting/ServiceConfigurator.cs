using BufTools.AspNet.InMemoryEndpointTesting.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BufTools.AspNet.TestFramework
{
    /// <summary>
    /// A class used by the <see cref="Browser{T}"/> to configure the injected services
    /// </summary>
    internal class ServiceConfigurator : IServiceConfigurator
    {
        private readonly IServiceCollection _services;

        /// <summary>
        /// Constructs an intance of an object
        /// </summary>
        /// <param name="services">A collection of services used dependency injection</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ServiceConfigurator(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Replaces an existing service registration with a new one
        /// </summary>
        /// <typeparam name="T">The type of the dependency</typeparam>
        /// <param name="instance">The object to register for injection</param>
        /// <returns>An <see cref="IServiceConfigurator"/> instance for chaining</returns>
        /// <exception cref="DependencyNotRegistered">Thrown when a dependency is supplied to replace a dependency that was not registered in the application itself</exception>
        /// <exception cref="NotImplementedException">Thrown if a new ServiceLifetime type has been added but is not supported by this method</exception>
        public IServiceConfigurator UseDependency<T>(T instance)
            where T : class
        {
            var descriptorToRemove = _services.FirstOrDefault(d => d.ServiceType == typeof(T));
            if (descriptorToRemove == null)
            {
                throw new DependencyNotRegistered(string.Format(FrameworkResources.DependencyNotRegisteredFormat, typeof(T).FullName));
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
