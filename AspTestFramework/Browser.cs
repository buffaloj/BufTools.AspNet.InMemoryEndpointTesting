using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;

namespace AspTestFramework
{
    public class Browser<TProgram>
        where TProgram : class
    {
        private readonly HttpClient _client;

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

        public RequestBuilder CreateRequest(string route)
        {
            return new RequestBuilder(_client, route);        
        }

    }
}
