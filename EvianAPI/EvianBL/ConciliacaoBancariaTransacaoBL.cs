using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using Evian.Entities.Entities.Enums;
using Evian.Repository.Core;
using System;
using System.Linq;

namespace EvianBL
{
    public class ConciliacaoBancariaTransacaoBL : EmpresaBL<ConciliacaoBancariaTransacaoDTO>
    {
        private ContaFinanceira contaFinanceira { get; set; }

        public ConciliacaoBancariaTransacaoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(ConciliacaoBancariaTransacaoDTO entity)
        {
            try
            {
                entity.EmpresaId = EmpresaId;
                entity.DataInclusao = DateTime.Now;
                entity.DataAlteracao = null;
                entity.DataExclusao = null;
                entity.UsuarioInclusao = AppUser;
                entity.UsuarioAlteracao = null;
                entity.UsuarioExclusao = null;
                entity.Ativo = true;
                entity.Id = Guid.NewGuid();

                var condicaoAVista = _unitOfWork.CondicaoParcelamentoBL.All.Where(x => x.Id == entity.CondicaoParcelamentoId && (x.QtdParcelas == 1 || x.CondicoesParcelamento.Equals("0"))).FirstOrDefault();
                if (condicaoAVista == null)
                    throw new Exception("Condição de parcelamento inválida para a transação. Somente a vista.");

                var conciliacaoBancariaId = _unitOfWork.ConciliacaoBancariaItemBL.All.Where(x => x.Id == entity.ConciliacaoBancariaItemId).FirstOrDefault().ConciliacaoBancariaId;

                var CBItemContaFinanceira = new ConciliacaoBancariaItemContaFinanceira()
                {
                    ConciliacaoBancariaItemId = entity.ConciliacaoBancariaItemId,
                    ValorConciliado = entity.ValorConciliado,
                };

                contaFinanceira.Id = Guid.NewGuid();
                contaFinanceira.ValorPrevisto = entity.ValorPrevisto;
                contaFinanceira.CategoriaId = entity.CategoriaId;
                contaFinanceira.FormaPagamentoId = entity.FormaPagamentoId;
                contaFinanceira.CondicaoParcelamentoId = entity.CondicaoParcelamentoId;
                contaFinanceira.PessoaId = entity.PessoaId;
                contaFinanceira.DataEmissao = DateTime.Now;
                contaFinanceira.DataVencimento = entity.DataVencimento;
                contaFinanceira.Descricao = entity.Descricao;
                contaFinanceira.StatusContaBancaria = StatusContaBancaria.Pago;
                contaFinanceira.ValorPago = entity.ValorPrevisto;

                if (entity.TipoContaFinanceira == "ContaPagar")
                    contaFinanceira.TipoContaFinanceira = TipoContaFinanceira.ContaPagar;
                else
                    contaFinanceira.TipoContaFinanceira = TipoContaFinanceira.ContaReceber;

                _unitOfWork.ContaFinanceiraBL.Insert(contaFinanceira);

                contaFinanceira.ValorPago = null;

                CBItemContaFinanceira.ContaFinanceira = contaFinanceira;

                _unitOfWork.ConciliacaoBancariaItemContaFinanceiraBL.Insert(CBItemContaFinanceira);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override void Update(ConciliacaoBancariaTransacaoDTO entity)
        {
            throw new Exception("Não é possível atualizar este tipo de registro");
        }

        public override void Delete(ConciliacaoBancariaTransacaoDTO entity)
        {
            throw new Exception("Não é possível deletar este tipo de registro");
        }
    }
}