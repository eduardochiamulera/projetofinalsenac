using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class ExtratoDetalheDTO
    {
        [JsonProperty("contaBancariaId")]
        public Guid ContaBancariaId { get; set; }

        [JsonProperty("contaBancariaDescricao")]
        public string ContaBancariaDescricao { get; set; }

        [JsonProperty("data")]
        public DateTime DataMovimento { get; set; }

        [JsonIgnore]
        public DateTime DataInclusao { get; set; }

        [JsonProperty("contaFinanceiraNumero")]
        public string ContaFinanceiraNumero { get; set; }

        [JsonProperty("descricaoLancamento")]
        public string DescricaoLancamento { get; set; }

        [JsonProperty("pessoaNome")]
        public string PessoaNome { get; set; }

        [JsonProperty("valorLancamento")]
        public decimal ValorLancamento { get; set; }
    }
}