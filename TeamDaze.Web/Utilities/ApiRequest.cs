using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TeamDaze.Api.Utilities
{
    public class ApiRequest
    {


        public enum Verbs
        {
            GET = 0,
            POST = 1,
            PUT = 2,
            DELETE = 3,
            HEAD = 4,
            OPTIONS = 5,
            PATCH = 6,
            MERGE = 7,
            COPY = 8
        }

        public object Data { get; set; }
        public string Url { get; set; }

        public ApiRequest(string url)
        {
            this.Url = url;
        }

        public async Task<HttpResponseMessage> MakeHttpClientRequest(object data = null, Verbs method = Verbs.POST, Dictionary<string, string> headers = null)
        {
            HttpResponseMessage response = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (headers != null)
                {
                    foreach (var header in headers)
                    {

                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                if (method.Equals(Verbs.POST))
                {
                    if (data == null)
                        data = new { };

                    response = await httpClient.PostAsync($"{Url}", content);


                }
                else if (method.Equals(Verbs.GET))
                {
                    response = await httpClient.GetAsync(this.Url);
                }
                return response;
            }

        }
    }

}