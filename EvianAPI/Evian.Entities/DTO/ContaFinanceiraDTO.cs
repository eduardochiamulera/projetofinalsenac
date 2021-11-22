using Evian.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        [JsonIgnore]
        public DateTime DataEmissao { get; set; }

        [JsonIgnore]
        public DateTime DataVencimento { get; set; }

        [JsonProperty("dataEmissao")]
        public string DataEmissaoString 
        {
            get => this.DataEmissao.ToString("yyyy-MM-dd");
            set => DataEmissao = DateTime.Parse(DataEmissaoString);
        }

        [JsonProperty("dataVencimento")]
        public string DataVencimentoString 
        { 
            get => this.DataVencimento.ToString("yyyy-MM-dd");
            set => DataVencimento = DateTime.Parse(DataVencimentoString);
        }

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

        [JsonProperty("categoriaNome")]
        public string CategoriaNome { get; set; }

        [JsonProperty("condicaoParcelamentoNome")]
        public string CondicaoParcelamentoNome { get; set; }

        [JsonProperty("pessoaNome")]
        public string PessoaNome { get; set; }

        [JsonProperty("formaPagamentoNome")]
        public string FormaPagamentoNome { get; set; }

        [JsonProperty("baixas")]
        public List<ContaFinanceiraBaixaDTO> Baixas { get; set; }
    }
}