﻿using Evian.Entities.Base;
using Evian.Entities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.DTO
{
    public class MovimentacaoFinanceiraPorCategoria : EmpresaBase
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