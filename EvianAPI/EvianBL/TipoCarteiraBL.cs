using Evian.Entities.Entities.Enums;
using System;

namespace EvianBL
{
    public static class TipoCarteiraBL
    {
        public static void ValidaTipoCarteira(TipoCarteira tipoCarteira)
        {
            if (!Enum.IsDefined(typeof(TipoCarteira), tipoCarteira))
                throw new Exception("Não foi possível inserir este registro. Tipo carteira inválido.");
        }
    }
}
