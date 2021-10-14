using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities.Entities.DTO
{
    public class ConciliacaoBancariaItemContaFinanceiraDTO : BaseDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonProperty("conciliacaoBancariaItemId")]
        public Guid ConciliacaoBancariaItemId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonProperty("contaFinanceiraId")]
        public Guid ContaFinanceiraId { get; set; }

        [JsonProperty("contaFinanceiraBaixaId")]
        public Guid? ContaFinanceiraBaixaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonProperty("valorConciliado")]
        public decimal ValorConciliado { get; set; }

        [JsonProperty("contaFinanceira")]
        public virtual ContaFinanceiraDTO ContaFinanceira { get; set; }
        [JsonProperty("contaFinanceiraBaixa")]
        public virtual ContaFinanceiraBaixaDTO ContaFinanceiraBaixa { get; set; }
    }
}