﻿using Evian.Entities.Entities;
using Evian.Entities.Enums;
using Evian.Notifications;
using Evian.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class ConciliacaoBancariaItemBL : EmpresaBL<ConciliacaoBancariaItem>
    {
        public ConciliacaoBancariaItemBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void ValidaModel(ConciliacaoBancariaItem entity)
        {
            entity.Fail(All.Any(x => x.OfxLancamentoMD5 == entity.OfxLancamentoMD5 && x.ConciliacaoBancariaId == entity.ConciliacaoBancariaId && x.Id != entity.Id), LancamentoJaImportado);

            base.ValidaModel(entity);
        }

        //public void SalvarConciliacaoBancariaItensExtrato(Guid conciliacaoBancariaId, List<OFXLancamento> lancamentos)
        //{
        //    var conciliacaoBancariaItens = lancamentos.Select(
        //        x => new ConciliacaoBancariaItem
        //        {
        //            ConciliacaoBancariaId = conciliacaoBancariaId,
        //            Data = x.Data,
        //            Descricao = x.Descricao,
        //            Valor = x.Valor,
        //            OfxLancamentoMD5 = x.MD5,
        //            StatusConciliado = StatusConciliado.Nao,
        //            Ativo = true
        //        }
        //    ).ToList();

        //    //Para cada lançamento extraído do extrato, verifica se o mesmo já não existe
        //    //pois pode importar um mesmo range de lançamentos em extratos diferentes,
        //    //exemplo de extratos gerados em períodos que coincidam
        //    foreach (var item in conciliacaoBancariaItens)
        //    {
        //        if (!All.Any(x => x.ConciliacaoBancariaId == conciliacaoBancariaId & x.OfxLancamentoMD5 == item.OfxLancamentoMD5))
        //        {
        //            Insert(item);
        //        }
        //    }
        //}

        public override void Update(ConciliacaoBancariaItem entity)
        {
            if (entity.ConciliacaoBancariaItemContasFinanceiras == null)
                throw new Exception("Não é possível atualizar um lançamento da conciliação bancária");
        }

        public override void Delete(ConciliacaoBancariaItem entityToDelete)
        {
            //conciliado sim e parcial não pode deletar
            if (AllIncluding(
                    x => x.ConciliacaoBancariaItemContasFinanceiras
                ).Where(x => x.Id == entityToDelete.Id).FirstOrDefault().ConciliacaoBancariaItemContasFinanceiras.Any(y => y.Ativo))
            {
                throw new Exception("Não é possível excluir um lançamento com baixas conciliadas");
            }
            else if (entityToDelete.StatusConciliado != StatusConciliado.Nao)
            {
                throw new Exception("Não é possível excluir um lançamento já conciliado");
            }
            else
            {
                base.Delete(entityToDelete);
            }
        }

        public static Error LancamentoJaImportado = new Error("Já existe um lançamento importado nesta conciliação, com a mesma data, valor e descrição (MD5 hash).");
    }
}