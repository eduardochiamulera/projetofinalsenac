using Evian.Entities.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities
{
    public class TransferenciaFinanceira : EmpresaBase
    {
        public MovimentacaoFinanceira MovimentacaoOrigem { get; set; }

        public MovimentacaoFinanceira MovimentacaoDestino { get; set; }
    }
}