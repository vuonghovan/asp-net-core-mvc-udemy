using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configs;

namespace Factories.Services
{
    public class HttpClientService
    {
        private HttpClient Client { get; }

        public HttpClientService(HttpClient client)
        {
            var config = ModuleCommon.Configuration.GetSection("HttpClientUrlAddress");

            if (config == null)
                throw new Exception("Web service config not found appsetting.json!");

            client.BaseAddress = new Uri(config.Value);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }

        public Task PostAsync(string url, string data)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(data, Encoding.UTF8, "application/json")
                };
                var respone = Client.SendAsync(request).Result;
                if (respone.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Cannot connect to Web Service API");
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
