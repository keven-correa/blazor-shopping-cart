using System.Text.Json;
using System.Text;

namespace Sales.WEB.Repository
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessages<T>> Get<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp, _jsonDefaultOptions);
                return new HttpResponseMessages<T>(response, false, responseHttp);
            }
            return new HttpResponseMessages<T>(default, true, responseHttp);
        }

        public async Task<HttpResponseMessages<object>> Post<T>(string url, T model)
        {
            var jsonMessage = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            return new HttpResponseMessages<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseMessages<TResponse>> Post<T, TResponse>(string url, T model)
        {
            var jsonMessage = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<TResponse>(responseHttp, _jsonDefaultOptions);
                return new HttpResponseMessages<TResponse>(response, false, responseHttp);
            }
            return new HttpResponseMessages<TResponse>(default, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            var respuestaString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(respuestaString, jsonSerializerOptions)!;
        }
    }

}

