using Newtonsoft.Json;

namespace SP.Contract.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T item) =>
           JsonConvert.SerializeObject(item);

        public static T FromJson<T>(this string item) =>
            JsonConvert.DeserializeObject<T>(item);
    }
}
