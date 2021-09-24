using Evian.Entities.Base;
using Evian.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities
{
    public class ContaPagar : EmpresaBase
    {
        public Guid? ContaPagarRepeticaoPaiId { get; set; }

        public Guid? ContaPagarParcelaPaiId { get; set; }

        public decimal ValorPrevisto { get; set; }

        public decimal? ValorPago { get; set; }

        public decimal Saldo { get; set; }

        public Guid CategoriaId { get; set; }

        public Guid CondicaoParcelamentoId { get; set; }

        public Guid PessoaId { get; set; }

        public DateTime DataEmissao { get; set; }

        public DateTime DataVencimento { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }

        public Guid FormaPagamentoId { get; set; }

        public TipoContaFinanceira TipoContaFinanceira { get; set; }

        public StatusContaBancaria StatusContaBancaria { get; set; }

        public bool Repetir { get; set; }

        public TipoPeriodicidade TipoPeriodicidade { get; set; }

        public int? NumeroRepeticoes { get; set; }

        public string DescricaoParcela { get; set; }

        public int Numero { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual CondicaoParcelamento CondicaoParcelamento { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual ContaPagar ContaPagarParcelaPai { get; set; }
        public virtual ContaPagar ContaPagarRepeticaoPai { get; set; }

    }
}
