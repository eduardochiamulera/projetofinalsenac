using Evian.Entities.Entities.Base;
using Evian.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Evian.Entities.Entities
{
    public class ConciliacaoBancariaItem : EmpresaBase
    {
        public Guid ConciliacaoBancariaId { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public string OfxLancamentoMD5 { get; set; }

        public StatusConciliado StatusConciliado { get; set; }

        public ConciliacaoBancaria ConciliacaoBancaria { get; set; }

        public virtual List<ConciliacaoBancariaItemContaFinanceira> ConciliacaoBancariaItemContasFinanceiras { get; set; }
    }
}