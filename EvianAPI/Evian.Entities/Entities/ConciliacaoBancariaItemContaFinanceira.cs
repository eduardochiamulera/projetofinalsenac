using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class ConciliacaoBancariaItemContaFinanceira : EmpresaBase
    {
        public Guid ConciliacaoBancariaItemId { get; set; }

        public Guid? ContaFinanceiraId { get; set; }

        public Guid? ContaFinanceiraBaixaId { get; set; }

        public decimal ValorConciliado { get; set; }

        public virtual ContaFinanceira ContaFinanceira { get; set; }

        public virtual ConciliacaoBancariaItem ConciliacaoBancariaItem { get; set; }

        public virtual ContaFinanceiraBaixa ContaFinanceiraBaixa { get; set; }
    }
}