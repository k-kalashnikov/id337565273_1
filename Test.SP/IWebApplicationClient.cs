using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SP
{
    public interface IWebApplicationClient
    {
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}
