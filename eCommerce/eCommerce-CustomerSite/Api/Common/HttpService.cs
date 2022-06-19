using eCommerce_CustomerSite.Extensions;
using eCommerce_SharedViewModels.EntitiesDto.Login;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eCommerce_CustomerSite.Api.Common
{
    public class HttpService
    {
        protected readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetToken<T>(string tokenEndpoint, LoginDto request)
        {
            var tokenResponse = await PostAsync<T>(tokenEndpoint, request);
            return tokenResponse;
        }


        public async Task<T> GetAsync<T>(string url, string accessToken = null)
        {
            await SetBearerToken(accessToken);

            using var response = await _httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAs<T>();
            return data;
        }

        public async Task<T> PostAsync<T>(string url, object data = null, string accessToken = null)
        {
            await SetBearerToken(accessToken);

            using var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (data != null)
            {
                request.Content = data.AsJsonContent();
            }

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                var createdObject = await response.Content.ReadAs<T>();
                return createdObject;
            }
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }

        public async Task<T> PutAsync<T>(string url, object data, string accessToken = null)
        {
            await SetBearerToken(accessToken);

            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = data.AsJsonContent();

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var updatedObject = await response.Content.ReadAs<T>();
            return updatedObject;
        }

        public async Task SoftDeleteAsync(string url, string accessToken = null)
        {
            await SetBearerToken(accessToken);

            using var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
        }


        protected Task SetBearerToken(string accessToken)
        {
            if (accessToken != null)
            {
                _httpClient.UseBearerToken(accessToken);
            }

            return Task.CompletedTask;
        }
    }
}
