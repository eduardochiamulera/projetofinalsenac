using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using Evian.Entities.Entities.Enums;
using Evian.Notifications;
using Evian.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvianBL
{
    public class MovimentacaoBL : EmpresaBL<MovimentacaoFinanceira>
    {
        public MovimentacaoBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(MovimentacaoFinanceira movimentacao)
        {
            movimentacao.Fail(movimentacao.ContaBancariaOrigemId == default(Guid) && movimentacao.ContaBancariaDestinoId == default(Guid), new Error("Conta Bancária Inválida."));
            movimentacao.Fail(movimentacao.CategoriaId == null, new Error("Informe a categoria financeira."));
            movimentacao.Fail(movimentacao.Data.Date > DateTime.Now.Date, new Error("Data da transação não pode ser superior a data atual"));

            var categoria = _unitOfWork.CategoriaBL.All.FirstOrDefault(x => x.Id == movimentacao.CategoriaId);

            if (categoria != null)
            {
                if (string.IsNullOrWhiteSpace(movimentacao.Descricao))
                    movimentacao.Descricao = categoria.Descricao;
            }

            movimentacao.UsuarioInclusao = AppUser;
            movimentacao.EmpresaId = EmpresaId;
            movimentacao.DataInclusao = DateTime.Now;
            movimentacao.Ativo = true;
            movimentacao.Descricao = movimentacao.Descricao.Substring(0, movimentacao.Descricao.Length > 200 ? 200 : movimentacao.Descricao.Length);

            base.ValidaModel(movimentacao);

            switch (categoria.TipoCarteira)
            {
                case TipoCarteira.Despesa:
                    Debito(movimentacao.Data, movimentacao.Valor, 
                        movimentacao.ContaBancariaOrigemId.Value, 
                        movimentacao.ContaFinanceiraId, movimentacao.Descricao, 
                        movimentacao.CategoriaId);
                    _unitOfWork.SaldoHistoricoBL.AtualizaSaldoHistorico(movimentacao.Data, (movimentacao.Valor), movimentacao.ContaBancariaOrigemId.Value, TipoCarteira.Despesa);
                    break;
                case TipoCarteira.Receita:
                    Credito(movimentacao.Data, movimentacao.Valor, movimentacao.ContaBancariaDestinoId.Value, 
                        movimentacao.ContaFinanceiraId, movimentacao.Descricao, 
                        movimentacao.CategoriaId);
                    _unitOfWork.SaldoHistoricoBL.AtualizaSaldoHistorico(movimentacao.Data, movimentacao.Valor, movimentacao.ContaBancariaDestinoId.Value, TipoCarteira.Receita);
                    break;
            }
        }

        public override void Update(MovimentacaoFinanceira entity)
        {
            throw new Exception("Não é possivel realizar a atualização de movimentação.");
        }

        public override void Delete(MovimentacaoFinanceira entityToDelete)
        {
            throw new Exception("Não é possivel realizar a deleção de movimentação.");
        }

        internal void CriaMovimentacao(DateTime dataMovimento, decimal valor, Guid contaBancariaId, TipoContaFinanceira tipoContaFinanceira, Guid contaFinanceiraId, string descricao = null)
        {
            switch (tipoContaFinanceira)
            {
                case TipoContaFinanceira.ContaPagar:
                    Debito(dataMovimento, valor, contaBancariaId, contaFinanceiraId, descricao, null);
                    break;
                case TipoContaFinanceira.ContaReceber:
                    Credito(dataMovimento, valor, contaBancariaId, contaFinanceiraId, descricao, null);
                    break;
                default:
                    throw new NotImplementedException("Erro ao criar movimentação financeira. O TipoContaFinanceira informado não é válido!");
            }
        }

        public void NovaTransferencia(TransferenciaFinanceira transferencia)
        {
            transferencia.EmpresaId = EmpresaId;
            transferencia.UsuarioInclusao = AppUser;

            var categoriaOrigem = _unitOfWork.CategoriaBL
                                    .All
                                    .FirstOrDefault(x => x.Id == transferencia.MovimentacaoOrigem.CategoriaId);

            if (categoriaOrigem?.TipoCarteira != TipoCarteira.Despesa)
            {
                throw new Exception("A categoria financeira origem dever ser do tipo de carteira despesa.");
            }

            var categoriaDestino = _unitOfWork.CategoriaBL
                                    .All
                                    .FirstOrDefault(x => x.Id == transferencia.MovimentacaoDestino.CategoriaId);

            if (categoriaDestino?.TipoCarteira != TipoCarteira.Receita)
            {
                throw new Exception("A categoria financeira destino dever ser do tipo de carteira receita.");
            }

            if (transferencia.MovimentacaoOrigem.ContaBancariaOrigemId == null || transferencia.MovimentacaoDestino.ContaBancariaDestinoId == null)
            {
                throw new Exception("Informe a conta bancária de origem na movimentação de origem e a conta bancária de destino na movimentação de destino.");
            }

            if (!Math.Abs(transferencia.MovimentacaoOrigem.Valor).Equals(Math.Abs(transferencia.MovimentacaoDestino.Valor)))
            {
                throw new Exception("O mesmo valor que será debitado de uma conta origem, deverá ser credidato em uma conta destino.");
            }

            if (!(transferencia.MovimentacaoOrigem.Valor < 0) || !(transferencia.MovimentacaoDestino.Valor > 0))
            {
                throw new Exception("O valor de origem deve ser negativo e o valor de destino deve ser positivo.");
            }

            if (transferencia.MovimentacaoOrigem.ContaBancariaOrigemId == null || transferencia.MovimentacaoOrigem.ContaBancariaDestinoId != null)
            {
                throw new Exception("A conta bancária de origem na movimentação de origem deve ser preenchida e a conta bancária de destino deve estar nula.");
            }

            if (transferencia.MovimentacaoDestino.ContaBancariaDestinoId == null || transferencia.MovimentacaoDestino.ContaBancariaOrigemId != null)
            {
                throw new Exception("A conta bancária de destino na movimentação de destino deve ser preenchida e a conta bancária de origem deve estar nula.");
            }

            if (transferencia.MovimentacaoDestino.ContaBancariaDestinoId == transferencia.MovimentacaoDestino.ContaBancariaOrigemId)
            {
                throw new Exception("A conta bancária de destino de origem não podem ser a mesma.");
            }

            transferencia.MovimentacaoOrigem.Valor *= -1;

            Insert(transferencia.MovimentacaoOrigem);
            Insert(transferencia.MovimentacaoDestino);
        }

        private MovimentacaoFinanceira Debito(DateTime data, decimal valor, Guid contaBancariaId, Guid? contaFinanceiraId = null, string descricao = null, Guid? categoriaId = null)
        {
            var mov = new MovimentacaoFinanceira()
            {
                ContaBancariaDestinoId = new Guid?(),
                ContaBancariaOrigemId = contaBancariaId,
                ContaFinanceiraId = contaFinanceiraId,
                CategoriaId = categoriaId,
                Data = data,
                Valor = valor * -1,
                Descricao = descricao
            };
            base.Insert(mov);

            return mov;
        }

        private MovimentacaoFinanceira Credito(DateTime data, decimal valor, Guid contaBancariaId, Guid? contaFinanceiraId = null, string descricao = null, Guid? categoriaId = null)
        {
            var mov = new MovimentacaoFinanceira()
            {
                ContaBancariaDestinoId = contaBancariaId,
                ContaBancariaOrigemId = new Guid?(),
                ContaFinanceiraId = contaFinanceiraId,
                CategoriaId = categoriaId,
                Data = data,
                Valor = valor,
                Descricao = descricao
            };
            base.Insert(mov);

            return mov;
        }
    }
}