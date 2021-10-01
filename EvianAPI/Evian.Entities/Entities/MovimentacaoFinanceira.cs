using Evian.Entities.Entities.Base;
using System;

namespace Evian.Entities.Entities
{
    public class MovimentacaoFinanceira : EmpresaBase
    {
        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public Guid? ContaBancariaOrigemId { get; set; }

        public Guid? ContaBancariaDestinoId { get; set; }

        public Guid? ContaFinanceiraId { get; set; }

        public Guid? CategoriaId { get; set; }

        public string Descricao { get; set; }

        public virtual ContaBancaria ContaBancariaOrigem { get; set; }

        public virtual ContaBancaria ContaBancariaDestino { get; set; }

        public virtual ContaFinanceira ContaFinanceira { get; set; }
    }
}