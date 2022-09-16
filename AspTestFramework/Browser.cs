using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace AspTestFramework
{
    /// <summary>
    /// A class that allows calling endpoints in an ASP application from a unit test and overriding dependency injection registrations with Mocks.
    /// </summary>
    /// <typeparam name="TProgram">The type of program that has endpoints to call</typeparam>
    public class Browser<TProgram>
        where TProgram : class
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Constructs an instance of a browser class
        /// </summary>
        /// <param name="action">Used to supply custom dependencies to use over normal app dependencies</param>
        public Browser(Action<IServiceConfigurator> action)
        {
            var application = new WebApplicationFactory<TProgram>()
                                    .WithWebHostBuilder(builder =>
                                    {
                                        builder.ConfigureServices(services =>
                                        {
                                            var configurator = new ServiceConfigurator(services);
                                            action.Invoke(configurator);
                                        });
                                    }); 

            _client = application.CreateClient();
        }

        /// <summary>
        /// Creates an endpoint request that can be built up and sent
        /// </summary>
        /// <param name="route"></param>
        /// <returns>A <see cref="RequestBuilder"/> to use for chaining calls together</returns>
        public RequestBuilder CreateRequest(string route)
        {
            return new RequestBuilder(_client, route);        
        }

    }
}
