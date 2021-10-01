using Evian.Repository.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvianBL
{
    public class UnitOfWork : IDisposable
    {
        public ApplicationDbContext _context;

        protected IEnumerable<EntityEntry> _contextChangeTrackerEntries()
        {
            return _context.ChangeTracker.Entries();
        }

        public UnitOfWork(ContextInitialize initialize)
        {
            _context = new ApplicationDbContext(initialize);
        }

        public void RejectChanges()
        {
            foreach (var entry in _contextChangeTrackerEntries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        {
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        }
                    case EntityState.Deleted:
                        {
                            entry.State = EntityState.Unchanged;
                            break;
                        }
                    case EntityState.Added:
                        {
                            entry.State = EntityState.Detached;
                            break;
                        }
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _contextDispose();
                }
            }
            _disposed = true;
        }
        private bool _disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void _contextDispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChanges();
        }

        public TBL GetGenericBL<TBL>()
        {
            try
            {
                return (TBL)GetType().GetProperty(typeof(TBL).Name).GetValue(this);
            }
            catch (Exception)
            {
                throw new Exception(string.Format("A classe {0} não está definida no {1}", typeof(TBL).Name, this.GetType().Name));
            }
        }

        #region BLS
        private PessoaBL pessoaBL;
        public PessoaBL PessoaBL => pessoaBL ?? (pessoaBL = new PessoaBL(_context, this));

        private ContaBancariaBL contaBancariaBL;
        public ContaBancariaBL ContaBancariaBL => contaBancariaBL ?? (contaBancariaBL = new ContaBancariaBL(_context, this));

        private CategoriaBL categoriaBL;
        public CategoriaBL CategoriaBL => categoriaBL ?? (categoriaBL = new CategoriaBL(_context, this));

        private BancoBL bancoBL;
        public BancoBL BancoBL => bancoBL ?? (bancoBL = new BancoBL(_context));

        private ContaFinanceiraBL contaFinanceiraBL;
        public ContaFinanceiraBL ContaFinanceiraBL => contaFinanceiraBL ?? (contaFinanceiraBL = new ContaFinanceiraBL(_context, this));

        private EstadoBL estadoBL;
        public EstadoBL EstadoBL => estadoBL ?? (estadoBL = new EstadoBL(_context));

        private CidadeBL cidadeBL;
        public CidadeBL CidadeBL => cidadeBL ?? (cidadeBL = new CidadeBL(_context));

        private CondicaoParcelamentoBL condicaoParcelamentoBL;
        public CondicaoParcelamentoBL CondicaoParcelamentoBL => condicaoParcelamentoBL ?? (condicaoParcelamentoBL = new CondicaoParcelamentoBL(_context, this));

        private SaldoHistoricoBL saldoHistoricoBL;
        public SaldoHistoricoBL SaldoHistoricoBL => saldoHistoricoBL ?? (saldoHistoricoBL = new SaldoHistoricoBL(_context, this));

        private MovimentacaoBL movimentacaoBL;
        public MovimentacaoBL MovimentacaoBL => movimentacaoBL ?? (movimentacaoBL = new MovimentacaoBL(_context, this));

        private ContaFinanceiraBaixaBL contaFinanceiraBaixaBL;
        public ContaFinanceiraBaixaBL ContaFinanceiraBaixaBL => contaFinanceiraBaixaBL ?? (contaFinanceiraBaixaBL = new ContaFinanceiraBaixaBL(_context, this));

        private ContaFinanceiraBaixaMultiplaBL contaFinanceiraBaixaMultiplaBL;
        public ContaFinanceiraBaixaMultiplaBL ContaFinanceiraBaixaMultiplaBL => contaFinanceiraBaixaMultiplaBL ?? (contaFinanceiraBaixaMultiplaBL = new ContaFinanceiraBaixaMultiplaBL(_context, this));

        private ConciliacaoBancariaBL conciliacaoBancariaBL;
        public ConciliacaoBancariaBL ConciliacaoBancariaBL => conciliacaoBancariaBL ?? (conciliacaoBancariaBL = new ConciliacaoBancariaBL(_context, this));

        private ConciliacaoBancariaItemBL conciliacaoBancariaItemBL;
        public ConciliacaoBancariaItemBL ConciliacaoBancariaItemBL => conciliacaoBancariaItemBL ?? (conciliacaoBancariaItemBL = new ConciliacaoBancariaItemBL(_context, this));

        private ConciliacaoBancariaItemContaFinanceiraBL conciliacaoBancariaItemContaFinanceiraBL;
        public ConciliacaoBancariaItemContaFinanceiraBL ConciliacaoBancariaItemContaFinanceiraBL => conciliacaoBancariaItemContaFinanceiraBL ?? (conciliacaoBancariaItemContaFinanceiraBL = new ConciliacaoBancariaItemContaFinanceiraBL(_context, this));

        private ConciliacaoBancariaTransacaoBL conciliacaoBancariaTransacaoBL;
        public ConciliacaoBancariaTransacaoBL ConciliacaoBancariaTransacaoBL => conciliacaoBancariaTransacaoBL ?? (conciliacaoBancariaTransacaoBL = new ConciliacaoBancariaTransacaoBL(_context, this));

        private ConciliacaoBancariaBuscarExistentesBL conciliacaoBancariaBuscarExistentesBL;
        public ConciliacaoBancariaBuscarExistentesBL ConciliacaoBancariaBuscarExistentesBL => conciliacaoBancariaBuscarExistentesBL ?? (conciliacaoBancariaBuscarExistentesBL = new ConciliacaoBancariaBuscarExistentesBL(_context, this));

        private FormaPagamentoBL formaPagamentoBL;
        public FormaPagamentoBL FormaPagamentoBL => formaPagamentoBL ?? (formaPagamentoBL = new FormaPagamentoBL(_context, this));

        private ExtratoBL extratoBL;
        public ExtratoBL ExtratoBL => extratoBL ?? (extratoBL = new ExtratoBL(_context, this));

        private FluxoCaixaBL fluxoCaixaBL;
        public FluxoCaixaBL FluxoCaixaBL => fluxoCaixaBL ?? (fluxoCaixaBL = new FluxoCaixaBL(_context, this));

        private TransferenciaBL transferenciaBL;
        public TransferenciaBL TransferenciaBL => transferenciaBL ?? (transferenciaBL = new TransferenciaBL(_context, this));

        private ReceitaPorCategoriaBL receitaPorCategoriaBL;
        public ReceitaPorCategoriaBL ReceitaPorCategoriaBL => receitaPorCategoriaBL ?? (receitaPorCategoriaBL = new ReceitaPorCategoriaBL(this));

        private DespesaPorCategoriaBL despesaPorCategoriaBL;
        public DespesaPorCategoriaBL DespesaPorCategoriaBL => despesaPorCategoriaBL ?? (despesaPorCategoriaBL = new DespesaPorCategoriaBL(this));

        //private MovimentacaoPorCategoriaBL movimentacaoPorCategoriaBL;
        //public MovimentacaoPorCategoriaBL MovimentacaoPorCategoriaBL => movimentacaoPorCategoriaBL ?? (movimentacaoPorCategoriaBL = new MovimentacaoPorCategoriaBL(this));

        private DashboardBL dashboardBL;
        public DashboardBL DashboardBL => dashboardBL ?? (dashboardBL = new DashboardBL(this));
        #endregion
    }
}
