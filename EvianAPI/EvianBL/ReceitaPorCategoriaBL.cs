using Evian.Entities.Base;
using Evian.Entities.DTO;
using Evian.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class ReceitaPorCategoriaBL : EmpresaBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ReceitaPorCategoriaBL(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<MovimentacaoFinanceiraPorCategoria> Get(DateTime dataInicial,
                                                  DateTime dataFinal,
                                                  bool somaRealizados = true,
                                                  bool somaPrevistos = true)
        {
            var contasReceber = _unitOfWork.ContaReceberBL
                                    .AllIncluding(c => c.Categoria)
                                    .Where(x => x.DataEmissao >= dataInicial &&
                                                x.DataEmissao <= dataFinal)
                                    .ToList();

            var receitasPorCategoria = contasReceber
                                        .Select(x => new
                                        {
                                            x.CategoriaId,
                                            x.Categoria,
                                            x.ValorPrevisto,
                                            x.ValorPago
                                        })
                                        .GroupBy(x => x.Categoria)
                                        .Select(g => new MovimentacaoFinanceiraPorCategoria()
                                        {
                                            Categoria = g.Key.Descricao,
                                            CategoriaId = g.Key.Id,
                                            CategoriaPaiId = g.Key.CategoriaPaiId,
                                            Previsto = g.Sum(s => s.ValorPrevisto - (s.ValorPago ?? 0)),
                                            Realizado = g.Sum(s => s.ValorPago ?? 0),
                                            Soma = g.Sum(s => s.ValorPrevisto),
                                            TipoCarteira = g.Key.TipoCarteira,
                                            TipoContaFinanceira = TipoContaFinanceira.ContaReceber,
                                        })
                                        .ToList();

            // Completa as categorias faltantes e sem movimentação
            var categoriasComReceita = contasReceber.Select(x => x.CategoriaId).Distinct();
            var categoriasSemReceita = _unitOfWork.CategoriaBL.All.Where(x => !categoriasComReceita.Contains(x.Id) && x.TipoCarteira == TipoCarteira.Receita);
            var receitasPorCategoriaZeradas = categoriasSemReceita
                                                .Select(x => new MovimentacaoFinanceiraPorCategoria()
                                                {
                                                    Categoria = x.Descricao,
                                                    CategoriaId = x.Id,
                                                    CategoriaPaiId = x.CategoriaPaiId,
                                                    Previsto = 0,
                                                    Realizado = 0,
                                                    Soma = 0,
                                                    TipoCarteira = x.TipoCarteira,
                                                    TipoContaFinanceira = TipoContaFinanceira.ContaReceber,
                                                })
                                                .ToList();
            receitasPorCategoria.AddRange(receitasPorCategoriaZeradas);

            // Ordena as categorias por pai e filho
            var pais = _unitOfWork.CategoriaBL
                        .All
                        .Where(e => e.CategoriaPaiId == null &&
                                    e.TipoCarteira == TipoCarteira.Receita)
                        .Select(x => new MovimentacaoFinanceiraPorCategoria()
                        {
                            Categoria = x.Descricao,
                            CategoriaId = x.Id,
                            CategoriaPaiId = x.CategoriaPaiId,
                            Previsto = contasReceber
                                    .Where(r => r.CategoriaId == x.Id ||
                                                r.Categoria.CategoriaPaiId == x.Id)
                                    .Sum(s => s.ValorPrevisto - (s.ValorPago ?? 0)),
                            Realizado = contasReceber
                                    .Where(r => r.CategoriaId == x.Id ||
                                                r.Categoria.CategoriaPaiId == x.Id)
                                    .Sum(s => s.ValorPago ?? 0),
                            Soma = contasReceber.Where(r => r.CategoriaId == x.Id || r.Categoria.CategoriaPaiId == x.Id).Sum(s => s.ValorPrevisto),
                            TipoCarteira = x.TipoCarteira,
                            TipoContaFinanceira = TipoContaFinanceira.ContaReceber
                        })
                        .OrderBy(x => x.Categoria)
                        .ToList();

            var listaOrdenada = new List<MovimentacaoFinanceiraPorCategoria>();

            foreach (var categoria in pais)
            {
                listaOrdenada.Add(categoria);

                foreach (var catFilho in receitasPorCategoria
                                            .Where(x => x.CategoriaPaiId == categoria.CategoriaId)
                                            .OrderBy(x => x.Categoria)
                                            .ToList())
                {
                    listaOrdenada.Add(catFilho);
                }
            }

            var jaOrdenados = listaOrdenada.Select(x => x.CategoriaId).ToArray();
            var semPaisNemFilhos = receitasPorCategoria.Where(x => !jaOrdenados.Contains(x.CategoriaId));
            listaOrdenada.AddRange(semPaisNemFilhos);

            try
            {
                return listaOrdenada;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}