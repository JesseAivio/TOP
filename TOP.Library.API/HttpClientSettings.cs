using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TOP.Library.API
{
    public class HttpClientSettings
    {
        public static HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(Url.TOP_API_Address)
        };
        public HttpClientSettings()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
        }
    }
}
