using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BufTools.AspNet.TestFramework
{
    /// <summary>
    /// Builds an HTTP request to be sent to an application endpoint
    /// </summary>
    public class RequestBuilder
    {
        private HttpClient _client;
        private readonly string _uri;
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly Dictionary<string, string?> _queryParams = new Dictionary<string, string?>();

        private HttpContent? _content;
        private string? _contentType;

        /// <summary>
        /// Constructs an instance of an object
        /// </summary>
        /// <param name="client">The client to use to send http requests</param>
        /// <param name="uri">The URI to send the request to</param>
        /// <exception cref="ArgumentNullException"></exception>
        internal RequestBuilder(HttpClient client, string uri)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        /// <summary>
        /// Supplies the request with a JSON string and sets the content type
        /// </summary>
        /// <param name="jsonString">The JSON string to use as content</param>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public RequestBuilder WithJsonContent(string jsonString)
        {
            SetStringContent(jsonString, "application/json");
            return this;
        }

        /// <summary>
        /// Sets a content string and associated content type
        /// </summary>
        /// <param name="content">A string containing the content to include in the request</param>
        /// <param name="type">The type fo the content</param>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        private RequestBuilder SetStringContent(string content, string type)
        {
            _contentType = type;

            if (content != null)
            {
                _content = new StringContent(content, Encoding.UTF8, type);
            }

            return this;
        }

        /// <summary>
        /// Adds a header to the request
        /// </summary>
        /// <param name="key">The name of the header</param>
        /// <param name="value">The associated value of the header</param>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public RequestBuilder WithHeader(string key, string value)
        {
            _headers.Add(key, value);
            return this;
        }

        /// <summary>
        /// Adds a query parameter use in the URI of the request
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the parameter</typeparam>
        /// <param name="name">The name of the parameter</param>
        /// <param name="value">The value associated with the parameter</param>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public RequestBuilder WithQueryParam<T>(string name, T value)
        {
            _queryParams[name] = value?.ToString();
            return this;
        }

        /// <summary>
        /// Adds a query parameter use in the URI of the request
        /// </summary>
        /// <param name="name">The name of the parameter</param>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public RequestBuilder WithQueryParam(string name)
        {
            _queryParams[name] = null;
            return this;
        }

        /// <summary>
        /// Sends tee request as a GET request
        /// </summary>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public Task<HttpResponseMessage> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendRequestAsync(HttpMethod.Get, cancellationToken);
        }

        /// <summary>
        /// Sends tee request as a PUT request
        /// </summary>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public Task<HttpResponseMessage> PutAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendRequestAsync(HttpMethod.Put, cancellationToken);
        }

        /// <summary>
        /// Sends tee request as a POST request
        /// </summary>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public Task<HttpResponseMessage> PostAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendRequestAsync(HttpMethod.Post, cancellationToken);
        }

        /// <summary>
        /// Sends tee request as a DELETE request
        /// </summary>
        /// <returns>A <see cref="RequestBuilder"/> instance for chaining</returns>
        public Task<HttpResponseMessage> DeleteAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return SendRequestAsync(HttpMethod.Delete, cancellationToken);
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpMethod verb, CancellationToken cancellationToken = default(CancellationToken))
        {
            var uri = _uri.WithQueryParams(_queryParams);

            using (var request = new HttpRequestMessage(verb, uri))
            {
                if (_content != null && !string.IsNullOrWhiteSpace(_contentType))
                {
                    request.Content = _content;
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue(_contentType);
                }

                foreach (var header in _headers)
                    request.Headers.Add(header.Key, header.Value);

                return await _client.SendAsync(request, cancellationToken);
            }
        }
    }
}