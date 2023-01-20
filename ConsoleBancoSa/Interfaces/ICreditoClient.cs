using ConsoleBancoSa.Models;
using System.Threading.Tasks;

namespace ConsoleBancoSa.Interfaces
{
    public interface ICreditoClient
    {
        Task<CreditoResponse> ValidaCredito(CreditoRequest request);
    }
}
