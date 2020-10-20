using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace SP.Contract.Common.Extensions
{
    public class JsonContent<T> : StringContent
        where T : class
    {
        public JsonContent(T obj)
            : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        {
        }
    }
}
