using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SP
{
    public class AuthClient : IDisposable
    {
        private readonly Uri _url;
        private string _login;
        private string _password;
        private HttpClient _httpClient;
        private string _token;

        public AuthClient(Uri url, string login, string password)
        {
            _url = url;
            _login = login;
            _password = password;
            _httpClient = new HttpClient { BaseAddress = _url };
        }

        public async Task<string> AuthenticateAsync()
        {
            if (_token == null)
            {
                if (await SigninAsync())
                {
                    _token = await ConfirmAsync();
                }
                else
                {
                    throw new InvalidOperationException(
                        "Authentication failed.");
                }
            }

            return _token;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        private async Task<bool> SigninAsync()
        {
            var loginData = new { Login = _login, Password = _password };
            var loginJson = JsonSerializer.Serialize(loginData);
            using var content = new StringContent(loginJson, Encoding.UTF8, "application/json");

            var loginResponse = await _httpClient
                .PostAsync(new Uri("signin", UriKind.Relative), content);

            loginResponse.EnsureSuccessStatusCode();
            var resultJson = await loginResponse.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<bool>(resultJson);
            return result;
        }

        private async Task<string> ConfirmAsync()
        {
            var confirmData = new
            {
                Login = _login,
                Code = 1234
            };

            var confirmJson = JsonSerializer.Serialize(confirmData);
            using var content = new StringContent(confirmJson, Encoding.UTF8, "application/json");

            var confirmResponse = await _httpClient
                .PostAsync(new Uri("code/confirm", UriKind.Relative), content);

            confirmResponse.EnsureSuccessStatusCode();
            var tokenJson = await confirmResponse.Content.ReadAsStringAsync();
            var token = JsonSerializer.Deserialize<string>(tokenJson);
            return token;
        }
    }
}
