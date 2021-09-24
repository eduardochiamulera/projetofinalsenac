using Evian.Entities.Base;
using System;

namespace Evian.Entities
{
    public class ConciliacaoBancariaItemContaFinanceira : EmpresaBase
    {
        public Guid ConciliacaoBancariaItemId { get; set; }

        public Guid? ContaPagarId { get; set; }

        public Guid? ContaReceberId { get; set; }

        public Guid? ContaFinanceiraBaixaId { get; set; }

        public decimal ValorConciliado { get; set; }

        public virtual ContaPagar ContaPagar { get; set; }

        public virtual ContaReceber ContaReceber { get; set; }

        public virtual ConciliacaoBancariaItem ConciliacaoBancariaItem { get; set; }

        public virtual ContaFinanceiraBaixa ContaFinanceiraBaixa { get; set; }
    }
}