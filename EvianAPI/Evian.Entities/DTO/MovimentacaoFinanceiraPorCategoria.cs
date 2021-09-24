using Evian.Entities.Base;
using Evian.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities
{
    public class MovimentacaoFinanceiraPorCategoria : EmpresaBase
    {
        public Guid CategoriaId { get; set; }
        public string Categoria { get; set; }
        public Guid? CategoriaPaiId { get; set; }
        public double Previsto { get; set; }
        public double? Realizado { get; set; }
        public double? Soma { get; set; }
        public TipoCarteira TipoCarteira { get; set; }
        public TipoContaFinanceira TipoContaFinanceira { get; set; }
    }
}