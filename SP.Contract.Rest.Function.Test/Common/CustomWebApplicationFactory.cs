using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using SP.Contract.Rest.Function.Test.Models;

namespace SP.Contract.Rest.Function.Test.Common
{
    public class CustomWebApplicationFactory
        <TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync()
        {
            var client = CreateClient();

            var token = await GetAccessTokenAsync();
            client.SetBearerToken(token);

            return client;
        }

        private async Task<string> GetAccessTokenAsync()
        {
            using var client = new HttpClient();
            var model = SignInSmsCodeViewModel.Create(Const.Login, Const.Code);
            var response = await client.PostAsJsonAsync(Const.API, model);
            var token = await response.Content.ReadAsStringAsync();
            return token.Replace("\"", string.Empty);
        }
    }
}
