using System;

namespace Evian.Entities.Entities.DTO
{
    [Serializable]
    public class DemonstrativoResultadoExercicioDTO : BaseDTO
    {
        public decimal ReceitasPrevistas { get; set; }
        public decimal DespesasPrevistas { get; set; }
        public decimal TotalPrevisto { get; set; }
        public decimal? ReceitasRealizadas { get; set; }
        public decimal? DespesasRealizadas { get; set; }
        public decimal? TotalRealizado { get; set; }
        public decimal? ReceitasTotais { get; set; }
        public decimal? DespesasTotais { get; set; }
        public decimal? Total { get; set; }
    }
}