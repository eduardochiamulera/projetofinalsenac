using Evian.Entities.Entities;
using Evian.Repository.Core;
using System;

namespace EvianBL
{
    public class ConciliacaoBancariaBuscarExistentesBL : EmpresaBL<ConciliacaoBancariaItem>
    {
        public ConciliacaoBancariaBuscarExistentesBL(ApplicationDbContext context, UnitOfWork unitOfWork) : base(context, unitOfWork) { }

        public override void Insert(ConciliacaoBancariaItem entity)
        {
            try
            {
                //conciliacaoBancariaItemContaFinanceiraBL.SalvarConciliacaoBuscarExistentes(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override void Update(ConciliacaoBancariaItem entity)
        {
            throw new Exception("Não é possível atualizar este tipo de registro");
        }

        public override void Delete(ConciliacaoBancariaItem entity)
        {
            throw new Exception("Não é possível deletar este tipo de registro");
        }
    }
}