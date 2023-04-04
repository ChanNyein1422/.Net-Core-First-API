using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Infra.Helper
{
    public class ApiRequestFactory : IApiRequestFactory
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiRequestFactory(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> PostGetString<T>(string url, T entity, bool isCompressed = true)
        {
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post, url)
            {
                Headers =
                        {
                            { HeaderNames.Accept, "application/json" },
                            { HeaderNames.UserAgent, "Server" }
                        }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var poststring = new StringContent(
                                 JsonSerializer.Serialize(entity),
                                 Encoding.UTF8,
                                 Application.Json); // using static System.Net.Mime.MediaTypeNames;

            using var httpResponseMessage = await httpClient.PostAsync(url, poststring);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                return contentStream.ToString();
            }
            else
            {
                return null;
            }
        }
        public async Task<U> PostDiffRequest<T, U>(string url, T entity, bool isCompressed = true)
        {
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post, url)
            {
                Headers =
                        {
                            { HeaderNames.Accept, "application/json" },
                            { HeaderNames.UserAgent, "Server" }
                        }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var poststring = new StringContent(
                                 JsonSerializer.Serialize(entity),
                                 Encoding.UTF8,
                                 Application.Json); // using static System.Net.Mime.MediaTypeNames;

            using var httpResponseMessage = await httpClient.PostAsync(url, poststring);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var data = JsonSerializer.Deserialize<U>(contentStream);
                return data;
            }
            else
            {
                return default(U);
            }
        }

        public async Task<T> GetRequest<T>(string url, bool isCompressed = false, string rootUrl = null)
        {
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get, url)
            {
                Headers =
                        {
                            { HeaderNames.Accept, "application/json" },
                            { HeaderNames.UserAgent, "Server" }
                        }
            };

            var httpClient = _httpClientFactory.CreateClient();

            using var httpResponseMessage = await httpClient.GetAsync(url);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var data = JsonSerializer.Deserialize<T>(contentStream);
                return data;
            }
            else
            {
                return default(T);
            }

        }
        public async Task<T> PostRequest<T>(string url, T entity, string rootUrl = null)
        {
            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Post, url)
            {
                Headers =
                        {
                            { HeaderNames.Accept, "application/json" },
                            { HeaderNames.UserAgent, "Server" }
                        }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var poststring = new StringContent(
                                 JsonSerializer.Serialize(entity),
                                 Encoding.UTF8,
                                 Application.Json); // using static System.Net.Mime.MediaTypeNames;

            using var httpResponseMessage = await httpClient.PostAsync(url, poststring);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                var data = JsonSerializer.Deserialize<T>(contentStream);
                return data;
            }
            else
            {
                return default(T);
            }

        }
    }
}
