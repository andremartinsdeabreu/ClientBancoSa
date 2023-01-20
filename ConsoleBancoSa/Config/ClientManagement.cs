using ConsoleBancoSa.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ConsoleBancoSa.Config
{
    public abstract class ClientManagement
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration = Configuration.ConfigBuild();

        public ClientManagement(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected IHttpClientFactory ClientFactory => _httpClientFactory;
        protected IConfiguration Config => _configuration;

        protected HttpContent CreateHttpContent(CreditoRequest request)
        {
            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings()
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            //}
            );
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
