using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SP
{
    public class WebApplicationClient<TStartup> : IWebApplicationClient, IDisposable
        where TStartup : class
    {
        private readonly WebApplicationFactory<TStartup> _webApplicationFactory =
            new WebApplicationFactory<TStartup>();

        private readonly AuthClient _authClient = new AuthClient(
            new Uri("https://api-lht.stecpoint.ru/identity/api/v1/account-two-factor/"),
            login: "stec.superuser@mail.ru",
            password: "market2019");

        private HttpClient _httpClient;

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer", await _authClient.AuthenticateAsync());

            var response = await CreateClient().SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var msg = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException(
                    $"Error status code {response.StatusCode} " +
                    $"with response body: {msg}");
            }

            return response;
        }

        public void Dispose()
        {
            _webApplicationFactory.Dispose();
            _authClient.Dispose();
            _httpClient?.Dispose();
        }

        private HttpClient CreateClient() => _httpClient ??
            (_httpClient = _webApplicationFactory.CreateClient());
    }
}
