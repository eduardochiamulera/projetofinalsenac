using Evian.Entities.Entities;
using Evian.Repository.Core;
using EvianBL;
using System;

namespace EvianBL
{
    public class TransferenciaBL : EmpresaBL<TransferenciaFinanceira>
    {
        public TransferenciaBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(TransferenciaFinanceira entity) => _unitOfWork.MovimentacaoBL.NovaTransferencia(entity);

        public override void Update(TransferenciaFinanceira entity)
        {
            throw new Exception("Não é possivel realizar a atualização de movimentação.");
        }

        public override void Delete(TransferenciaFinanceira entityToDelete)
        {
            throw new Exception("Não é possivel realizar a deleção de movimentação.");
        }
    }
}