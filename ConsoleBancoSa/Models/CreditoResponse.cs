using ConsoleBancoSa.Enums;

namespace ConsoleBancoSa.Models
{
    public class CreditoResponse
    {
        public StatusCredito Status { get; set; }
        public double ValorTotal { get; set; }
        public double ValorJuros { get; set; }
    }
}
