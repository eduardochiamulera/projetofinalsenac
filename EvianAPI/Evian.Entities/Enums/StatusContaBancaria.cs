using Evian.Helpers;

namespace Evian.Entities.Enums
{
    public enum StatusContaBancaria
    {
        [Subtitle("EmAberto", "Em aberto", "ABER", "totvs-blue")]
        EmAberto = 1,

        [Subtitle("Pago", "Pago", "PAGO", "green")]
        Pago = 2,

        [Subtitle("BaixadoParcialmente", "Baixado Parcialmente", "BPAR", "gray")]
        BaixadoParcialmente = 3
    }
}
