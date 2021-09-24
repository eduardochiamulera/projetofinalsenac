using Newtonsoft.Json;

namespace Evian.Entities
{
    public class FluxoCaixaSaldo
    {
        [JsonProperty("saldoAtual")]
        public double SaldoAtual { get; set; }

        [JsonProperty("totalRecebimentos")]
        public double TotalRecebimentos { get; set; }

        [JsonProperty("totalPagamentos")]
        public double TotalPagamentos { get; set; }

        [JsonProperty("saldoProjetado")]
        public double SaldoProjetado { get; set; }
    }
}