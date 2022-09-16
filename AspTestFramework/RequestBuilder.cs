using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AspTestFramework
{
    public class RequestBuilder
    {
        private HttpClient _client;
        private readonly string _uri;
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        private HttpContent? _content;
        private string? _contentType;

        public RequestBuilder(HttpClient client, string uri)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public RequestBuilder WithJsonContent(string jsonString)
        {
            SetStringContent(jsonString, "application/json");
            return this;
        }

        private RequestBuilder SetStringContent(string content, string type)
        {
            _contentType = type;

            if (content != null)
            {
                _content = new StringContent(content, Encoding.UTF8, type);
            }

            return this;
        }

        public RequestBuilder WithHeader(string key, string value)
        {
            _headers.Add(key, value);
            return this;
        }

        public Task<HttpResponseMessage> GetAsync()
        {
            return SendRequestAsync(HttpMethod.Get);
        }

        public Task<HttpResponseMessage> PutAsync()
        {
            return SendRequestAsync(HttpMethod.Put);
        }

        public Task<HttpResponseMessage> PostAsync()
        {
            return SendRequestAsync(HttpMethod.Post);
        }

        public Task<HttpResponseMessage> DeleteAsync()
        {
            return SendRequestAsync(HttpMethod.Delete);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpMethod verb)
        {
            using (var request = new HttpRequestMessage(verb, _uri))
            {
                if (_content != null && !string.IsNullOrWhiteSpace(_contentType))
                {
                    request.Content = _content;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
                }

                foreach (var header in _headers)
                    request.Headers.Add(header.Key, header.Value);

                return await _client.SendAsync(request);
            }
        }
    }
}