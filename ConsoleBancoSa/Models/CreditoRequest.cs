using ConsoleBancoSa.Enums;
using System;

namespace ConsoleBancoSa.Models
{
    public class CreditoRequest
    {
        public double ValorCredito { get; set; }
        public TipoCredito TipoCredito { get; set; }
        public byte QuantidadeParcelas { get; set; }
        public DateTime PrimeiroVencimento { get; set; }
    }
}
