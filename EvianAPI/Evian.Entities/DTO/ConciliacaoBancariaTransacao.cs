using Evian.Entities.Base;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.DTO
{
    public class ConciliacaoBancariaTransacao : EmpresaBase
    {
        [JsonProperty("conciliacaoBancariaItemId")]
        public Guid ConciliacaoBancariaItemId { get; set; }

        [JsonProperty("contaFinanceiraId")]
        public Guid ContaFinanceiraId { get; set; }

        [JsonProperty("valorConciliado")]
        public decimal ValorConciliado { get; set; }

        [JsonProperty("valorPrevisto")]
        public decimal ValorPrevisto { get; set; }

        [JsonProperty("categoriaId")]
        public Guid CategoriaId { get; set; }

        [JsonProperty("formaPagamentoId")]
        public Guid FormaPagamentoId { get; set; }

        [JsonProperty("condicaoParcelamentoId")]
        public Guid CondicaoParcelamentoId { get; set; }

        [JsonProperty("pessoaId")]
        public Guid PessoaId { get; set; }

        [JsonProperty("dataEmissao")]
        [Column(TypeName = "date")]
        public DateTime DataEmissao { get; set; }

        [JsonProperty("dataVencimento")]
        [Column(TypeName = "date")]
        public DateTime DataVencimento { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("tipoContaFinanceira")]
        public string TipoContaFinanceira { get; set; }
    }
}