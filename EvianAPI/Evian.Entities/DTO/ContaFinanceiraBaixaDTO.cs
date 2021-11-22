using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class ContaFinanceiraBaixaDTO : BaseDTO
    {
        [JsonProperty("contaFinanceiraId")]
        public Guid ContaFinanceiraId { get; set; }

        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("observacaoBaixa")]
        public string Observacao { get; set; }
    }
}