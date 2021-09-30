using Evian.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities
{
    public class MovimentacaoFinanceira : EmpresaBase
    {
        public DateTime Data { get; set; }

        public decimal Valor { get; set; }

        public Guid? ContaBancariaOrigemId { get; set; }

        public Guid? ContaBancariaDestinoId { get; set; }

        public Guid? ContaReceberId { get; set; }

        public Guid? ContaPagarId { get; set; }

        public Guid? CategoriaId { get; set; }

        public string Descricao { get; set; }

        public virtual ContaBancaria ContaBancariaOrigem { get; set; }

        public virtual ContaBancaria ContaBancariaDestino { get; set; }

        public virtual ContaReceber ContaReceber { get; set; }

        public virtual ContaPagar ContaPagar { get; set; }
    }
}