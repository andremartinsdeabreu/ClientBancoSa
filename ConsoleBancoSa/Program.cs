using ConsoleBancoSa.Config;
using ConsoleBancoSa.Interfaces;
using ConsoleBancoSa.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace ConsoleBancoSa
{
    public class Program
    {
        private static readonly IHost _host = Configuration.HostBuild();
        private static async Task Main(string[] args)
        {
            while (true)
            {
                Cabecalho();

                double valor = ValorCredito();
                Enums.TipoCredito tipo = TipoCredito();
                byte quantidade = QuantidadeParcela();
                DateTime vencimento = DataVencimento();

                var request = new CreditoRequest()
                {
                    ValorCredito = valor,
                    TipoCredito = tipo,
                    QuantidadeParcelas = quantidade,
                    PrimeiroVencimento = vencimento
                };

                var retorno = await _host.Services.GetRequiredService<ICreditoClient>().ValidaCredito(request);
                Console.WriteLine();
                Console.WriteLine($"Status: {Enum.GetName<Enums.StatusCredito>(retorno.Status)}");
                Console.WriteLine($"Valor do Juros: {retorno.ValorJuros}");
                Console.WriteLine($"Valor Total: {retorno.ValorTotal}");
 
                if (Continua())
                    continue;

                Console.WriteLine("Precione alguma tecla para sair!");
                Console.ReadKey();
                break;
;            }
        }
        private static void Cabecalho()
        {
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("=-=-=-=-=-=-=   Banco Sa   -=-=-=-=-=-=-=");
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine();
 
            Console.WriteLine("Status para válidar o crédito:");
            Console.WriteLine("1 -> Crédito Direto");
            Console.WriteLine("2 -> Crédito Consignado");
            Console.WriteLine("3 -> Crédito Pessoa Jurídica");
            Console.WriteLine("4 -> Crédito Pessoa Física");
            Console.WriteLine("5 -> Crédito Imobiliário");
            Console.WriteLine();
        }

        private static bool Continua()
        {
            bool ok;
            while (true)
            {
                Console.WriteLine();
                Console.Write("Deseja continuar com a simulação [S/N]: ");
                string resposta = Console.ReadLine();
                if (!string.IsNullOrEmpty(resposta) && !string.IsNullOrWhiteSpace(resposta))
                {
                    ok = resposta.ToUpper() == "S";
                    break;
                }
                else
                {
                    Console.WriteLine("Informe S ou N");
                    ok = false;
                }
            }
            return ok;
        }

        private static double ValorCredito()
        {
            double valor;
            while (true)
            {
                try
                {
                    Console.Write("Valor do Crédito: ");
                    valor = Convert.ToDouble(Console.ReadLine());
                    if (valor <= 0)
                    {
                        Console.WriteLine("Informe um valor válido!");
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Informe um valor válido!");
                }
            }
            return valor;
        }
        private static Enums.TipoCredito TipoCredito()
        {
            Enums.TipoCredito tipo;
            while (true)
            {
                try
                {
                    Console.Write("Tipo do Crédito [1, 2, 3, 4, 5]: ");
                    int index = Convert.ToInt32(Console.ReadLine());
                    tipo = Enum.GetValues<Enums.TipoCredito>()[index - 1];
                    break;
                }
                catch
                {
                    Console.WriteLine("Informe um tipo válido!");
                }
            }
            return tipo;
        }
        private static byte QuantidadeParcela()
        {
            byte qtd;
            while (true)
            {
                try
                {
                    Console.Write("Quantidade de Parcelas: ");
                    qtd = Convert.ToByte(Console.ReadLine());
                    if (qtd <= 0)
                    {
                        Console.WriteLine("Informe uma quantidade válida!");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Informe uma quantidade válida!");
                }
            }
            return qtd;
        }
        private static DateTime DataVencimento()
        {
            DateTime data;
            while (true)
            {
                try
                {
                    Console.Write("Data da primeira parcela: ");
                    data = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Informe uma data válida!");
                }
            }
            return data;
        }
    }
}
