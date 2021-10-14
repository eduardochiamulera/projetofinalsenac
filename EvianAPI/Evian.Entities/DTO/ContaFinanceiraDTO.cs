using Evian.Helpers;
using Newtonsoft.Json;
using System;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class ContaFinanceiraDTO : BaseDTO
    {
        [JsonProperty("contaFinanceiraRepeticaoPaiId")]
        public Guid? ContaFinanceiraRepeticaoPaiId { get; set; }

        [JsonProperty("ContaFinanceiraParcelaPaiId")]
        public Guid? ContaFinanceiraParcelaPaiId { get; set; }

        [JsonProperty("valorPrevisto")]
        public decimal ValorPrevisto { get; set; }

        [JsonProperty("valorPago")]
        public decimal? ValorPago { get; set; }

        [JsonProperty("categoriaId")]
        public Guid CategoriaId { get; set; }

        [JsonProperty("condicaoParcelamentoId")]
        public Guid CondicaoParcelamentoId { get; set; }

        [JsonProperty("pessoaId")]
        public Guid PessoaId { get; set; }

        [JsonProperty("dataEmissao")]
        public DateTime DataEmissao { get; set; }

        [JsonProperty("dataVencimento")]
        public DateTime DataVencimento { get; set; }

        [JsonProperty("descricao")]
        public string Descricao { get; set; }

        [JsonProperty("observacao")]
        public string Observacao { get; set; }

        [JsonProperty("formaPagamentoId")]
        public Guid FormaPagamentoId { get; set; }

        [JsonProperty("statusContaBancaria")]
        [APIEnum("StatusContaBancaria")]
        public string StatusContaBancaria { get; set; }

        [JsonProperty("repetir")]
        public bool Repetir { get; set; }

        [JsonProperty("tipoPeriodicidade")]
        [APIEnum("TipoPeriodicidade")]
        public string TipoPeriodicidade { get; set; }

        [JsonProperty("numeroRepeticoes")]
        public int? NumeroRepeticoes { get; set; }

        [JsonProperty("descricaoParcela")]
        public string DescricaoParcela { get; set; }

        [JsonProperty("saldo")]
        public decimal Saldo { get; set; }

        [JsonIgnore]
        public int DiasVencidos
        {
            get
            {
                return (DataVencimento < DateTime.Now.Date) ? Convert.ToInt32((DateTime.Now.Date - DataVencimento).TotalDays) : 0;
            }

        }

        [JsonProperty("numero")]
        public int Numero { get; set; }

        [JsonProperty("contaFinanceiraRepeticaoPai")]
        public virtual ContaFinanceiraDTO ContaFinanceiraRepeticaoPai { get; set; }

        [JsonProperty("contaFinanceiraRepeticaoPai")]
        public virtual ContaFinanceiraDTO ContaFinanceiraParcelaPai { get; set; }

        [JsonProperty("categoria")]
        public virtual CategoriaDTO Categoria { get; set; }

        [JsonProperty("condicaoParcelamento")]
        public virtual CondicaoParcelamentoDTO CondicaoParcelamento { get; set; }

        [JsonProperty("pessoa")]
        public virtual PessoaDTO Pessoa { get; set; }

        [JsonProperty("formaPagamento")]
        public virtual FormaPagamentoDTO FormaPagamento { get; set; }

        [JsonProperty("contaBancaria")]
        public virtual ContaBancariaDTO ContaBancaria { get; set; }
    }
}