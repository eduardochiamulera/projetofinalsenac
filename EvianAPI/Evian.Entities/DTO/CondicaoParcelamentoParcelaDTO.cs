using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities.DTO
{
    public class CondicaoParcelamentoParcelaDTO
    {
        [JsonProperty("descricao")]
        public string DescricaoParcela { get; set; }

        [JsonIgnore]
        public DateTime DataVencimento { get; set; }

        [JsonProperty("dataVencimento")]
        public string DataVencimentoRest
        {
            get => this.DataVencimento.ToString("yyyy-MM-dd");
            set => DataVencimento = DateTime.Parse(DataVencimentoRest);
        }

        [JsonProperty("valor")]
        public decimal Valor { get; set; }
    }
}
