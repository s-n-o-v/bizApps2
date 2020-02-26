using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CoreLibrary
{

    public class RestService
    {
        private string p_URL;
        private HttpClient client;
        private HttpResponseMessage response;

        public RestService(string serviceUrl)
        {
            p_URL = serviceUrl;
            client = new HttpClient
            {
                BaseAddress = new Uri(p_URL)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public void Stop()
        {
            client.Dispose();
        }
        public string GetData(string urlParams)
        {
            var data = GetDataFromService(urlParams);
            return data;
        }
        private string GetDataFromService(string urlParams)
        {
            response = client.GetAsync(urlParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var obj = response.Content.ReadAsStringAsync().Result;
                response.Dispose();
                return obj;
            }
            else
            {
                throw new Exception("GetData. Ошибка чтения потока");
            }
        }

    }
}
