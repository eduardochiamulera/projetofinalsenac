using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class ContaFinanceiraBaixaDTO : EmpresaBaseDTO
    {
        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("contaFinanceiraId")]
        public Guid ContaFinanceiraId { get; set; }

        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("valor")]
        public double Valor { get; set; }

        [JsonProperty("observacao")]
        public string Observacao { get; set; }

        [JsonProperty("contaFinanceira")]
        public virtual ContaFinanceiraDTO ContaFinanceira { get; set; }

        [JsonProperty("contaBancaria")]
        public virtual ContaBancariaDTO ContaBancaria { get; set; }
    }
}