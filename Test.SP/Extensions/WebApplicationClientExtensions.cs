using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SP
{
    public static class WebApplicationClientExtensions
    {
        private static readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

        public static async Task<T> GetAsync<T>(this IWebApplicationClient client, Uri uri)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            using var response = await client.SendAsync(request);
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonDeserializeObject<T>(responseJson);
        }

        public static async Task<T> PostAsync<T>(this IWebApplicationClient client, Uri uri, object body)
        {
            var requestJsonContent = JsonSerializeObject(body);
            using var requestContent = new StringContent(requestJsonContent, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = requestContent
            };

            using var response = await client.SendAsync(request);
            var responseJsonContent = await response.Content.ReadAsStringAsync();
            return JsonDeserializeObject<T>(responseJsonContent);
        }

        private static string JsonSerializeObject(object value) =>
            JsonConvert.SerializeObject(value, _jsonSerializerSettings);

        private static T JsonDeserializeObject<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value, _jsonSerializerSettings);
    }
}
