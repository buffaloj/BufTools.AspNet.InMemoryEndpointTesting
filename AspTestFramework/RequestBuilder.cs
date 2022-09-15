using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspTestFramework
{
    public class RequestBuilder
    {
        private HttpClient _client;
        private readonly string _uri;

        public RequestBuilder(HttpClient client, string uri)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            return await _client.GetAsync(_uri);
        }
    }
}