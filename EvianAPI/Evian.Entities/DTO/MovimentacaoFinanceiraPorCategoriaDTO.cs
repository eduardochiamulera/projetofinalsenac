using Evian.Entities.Enums;
using System;

namespace Evian.Entities.Entities.DTO
{
    public class MovimentacaoFinanceiraPorCategoriaDTO : BaseDTO
    {
        public Guid CategoriaId { get; set; }
        public string Categoria { get; set; }
        public Guid? CategoriaPaiId { get; set; }
        public decimal Previsto { get; set; }
        public decimal? Realizado { get; set; }
        public decimal? Soma { get; set; }
        public TipoCarteira TipoCarteira { get; set; }
        public TipoContaFinanceira TipoContaFinanceira { get; set; }
    }
}