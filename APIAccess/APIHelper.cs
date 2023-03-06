using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace UniverusWebTelerik.APIAccess
{
    public class APIHelper: IAPIHelper
    {
        public APIHelper()
        {

        }

        public string CallAPIGet(string endpointurl, string param = "")
        {
            var URL = $"http://localhost:44233/api/{endpointurl}";
            var urlParameters = param;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;

            client.Dispose();

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }

            return string.Empty;
        }

        //public string CallAPIPost(string endpointurl, object paramObj)
        //{
        //    var URL = $"http://localhost:44233/api/{endpointurl}";

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(URL);

        //    // Add an Accept header for JSON format.
        //    client.DefaultRequestHeaders.Accept.Add(
        //    new MediaTypeWithQualityHeaderValue("application/json"));
        //    var content = JsonConvert.SerializeObject(paramObj);

        //    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
        //    var byteContent = new ByteArrayContent(buffer);



        //    var result = client.PostAsync("", byteContent).Result;

        //    return string.Empty;
        //}

        public async Task CallAPIPost(string endpointurl, object paramObj)
        {
            var URL = $"http://localhost:44233/api/{endpointurl}";

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), URL))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    var content = JsonConvert.SerializeObject(paramObj);

                    request.Content = new StringContent(content);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);
                }
            }
        }
    }
}