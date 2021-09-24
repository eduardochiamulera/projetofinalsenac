using Evian.Helpers;

namespace Evian.Entities.Enums
{
    public enum TipoContaFinanceiraVM
    {
        [Subtitle("ContaPagar", "Conta Pagar")]
        ContaPagar = 1,

        [Subtitle("ContaReceber", "Conta Receber")]
        ContaReceber = 2,

        [Subtitle("Transferencia", "Transferência")]
        Transferencia = 3
    }
}