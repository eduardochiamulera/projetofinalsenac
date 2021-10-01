using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class MovimentacaoDTO : EmpresaBaseDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonProperty("data")]
        public DateTime Data { get; set; }

        [JsonProperty("valor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public double Valor { get; set; }

        [JsonProperty("contaBancariaOrigemId")]
        public Guid? ContaBancariaOrigemId { get; set; }

        [JsonProperty("contaBancariaDestinoId")]
        public Guid? ContaBancariaDestinoId { get; set; }

        [JsonProperty("contaFinanceiraId")]
        public Guid? ContaFinanceiraId { get; set; }

        [JsonProperty("categoriaId")]
        public Guid? CategoriaId { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("contaBancariaOrigem")]
        public virtual ContaBancariaDTO ContaBancariaOrigem { get; set; }

        [JsonProperty("contaBancariaDestino")]
        public virtual ContaBancariaDTO ContaBancariaDestino { get; set; }

        [JsonProperty("contaFinanceira")]
        public virtual ContaFinanceiraDTO ContaFinanceira { get; set; }

        [JsonProperty("categoria")]
        public virtual CategoriaDTO Categoria { get; set; }
    }
}