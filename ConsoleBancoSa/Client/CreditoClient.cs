using ConsoleBancoSa.Config;
using ConsoleBancoSa.Interfaces;
using ConsoleBancoSa.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleBancoSa.Client
{
    public class CreditoClient : ClientManagement, ICreditoClient
    {
        public CreditoClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<CreditoResponse> ValidaCredito(CreditoRequest request)
        {
            CreditoResponse response = null;
            // Call the method Get
            var responseService = await ClientFactory.CreateClient().PostAsync(Config["HttpBancoSaClient"], CreateHttpContent(request));

            // Check the response
            if (responseService.IsSuccessStatusCode)
            {
                // Read the list of Users
                var outputService = await responseService.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                try
                {
                    // Deserialization of the output
                    response = System.Text.Json.JsonSerializer.Deserialize<CreditoResponse>(outputService, options);
                }
                catch
                {
                    throw;
                }
            }

            return response;
        }
    }
}
