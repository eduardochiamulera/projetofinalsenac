using Evian.Entities;
using Evian.Entities.DTO;
using Evian.Entities.Enums;
using Evian.Repository.Core;
using System;
using System.Linq;

namespace EvianBL
{
    public class ConciliacaoBancariaTransacaoBL : EmpresaBL<ConciliacaoBancariaTransacao>
    {
        private ContaPagar contaPagar { get; set; }
        private ContaReceber contaReceber { get; set; }

        public ConciliacaoBancariaTransacaoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(ConciliacaoBancariaTransacao entity)
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

                if (entity.TipoContaFinanceira == "ContaPagar")
                {
                    contaPagar.Id = Guid.NewGuid();
                    contaPagar.ValorPrevisto = entity.ValorPrevisto;
                    contaPagar.CategoriaId = entity.CategoriaId;
                    contaPagar.FormaPagamentoId = entity.FormaPagamentoId;
                    contaPagar.CondicaoParcelamentoId = entity.CondicaoParcelamentoId;
                    contaPagar.PessoaId = entity.PessoaId;
                    contaPagar.DataEmissao = DateTime.Now;
                    contaPagar.DataVencimento = entity.DataVencimento;
                    contaPagar.Descricao = entity.Descricao;
                    contaPagar.StatusContaBancaria = StatusContaBancaria.Pago;
                    contaPagar.ValorPago = entity.ValorPrevisto;
                    _unitOfWork.ContaPagarBL.Insert(contaPagar);

                    contaPagar.ValorPago = null;
                    CBItemContaFinanceira.ContaPagar = contaPagar;
                }
                else
                {
                    contaReceber.Id = Guid.NewGuid();
                    contaReceber.ValorPrevisto = entity.ValorPrevisto;
                    contaReceber.CategoriaId = entity.CategoriaId;
                    contaReceber.FormaPagamentoId = entity.FormaPagamentoId;
                    contaReceber.CondicaoParcelamentoId = entity.CondicaoParcelamentoId;
                    contaReceber.PessoaId = entity.PessoaId;
                    contaReceber.DataEmissao = DateTime.Now;
                    contaReceber.DataVencimento = entity.DataVencimento;
                    contaReceber.Descricao = entity.Descricao;
                    contaReceber.StatusContaBancaria = StatusContaBancaria.Pago;
                    contaReceber.ValorPago = entity.ValorPrevisto;
                    _unitOfWork.ContaReceberBL.Insert(contaReceber);
                    
                    contaReceber.ValorPago = null;

                    CBItemContaFinanceira.ContaReceber = contaReceber;
                }

                _unitOfWork.ConciliacaoBancariaItemContaFinanceiraBL.Insert(CBItemContaFinanceira);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override void Update(ConciliacaoBancariaTransacao entity)
        {
            throw new Exception("Não é possível atualizar este tipo de registro");
        }

        public override void Delete(ConciliacaoBancariaTransacao entity)
        {
            throw new Exception("Não é possível deletar este tipo de registro");
        }
    }
}