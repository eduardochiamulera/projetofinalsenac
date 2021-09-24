using Evian.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities
{
    public class TransferenciaFinanceira : EmpresaBase
    {
        public MovimentacaoFinanceira MovimentacaoOrigem { get; set; }

        public MovimentacaoFinanceira MovimentacaoDestino { get; set; }
    }
}