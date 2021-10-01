using Evian.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Evian.Repository.Core
{
    public class ApplicationDbContext : DbContext
    {
        private Guid _empresaId;
        public Guid EmpresaId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_empresaId.ToString()))
                    throw new ApplicationException("ERRO! EmpresaId não informado.");

                return _empresaId;
            }
            set
            {
                _empresaId = value;
            }
        }

        private string _appUser;
        public string AppUser
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_appUser))
                    throw new ApplicationException("ERRO! AppUser não informado.");

                return _appUser;
            }
            set
            {
                _appUser = value;
            }
        }

        public ApplicationDbContext(ContextInitialize initialize)
        {
            AppUser = initialize.AppUser;
            EmpresaId = initialize.EmpresaId;
        }

        public DbSet<Pais> Pais { get; set; }
        public DbSet<Pais> Cidades { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<ContaBancaria> ContasBancarias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CondicaoParcelamento> CondicoesParcelamento { get; set; }
        public DbSet<ContaFinanceira> ContasFinanceiras { get; set; }
        public DbSet<ContaFinanceiraBaixa> ContasFinanceirasBaixas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<SaldoHistorico> SaldosHistorico { get; set; }
        public DbSet<ConciliacaoBancaria> ConciliacoesBancarias { get; set; }
        public DbSet<ConciliacaoBancariaItem> ConciliacaoBancariaItens { get; set; }
        public DbSet<ConciliacaoBancariaItemContaFinanceira> ConciliacaoBancariaItemContasFinanceiras { get; set; }
        public DbSet<FormaPagamento> FormasPagamento { get; set; }
        public DbSet<MovimentacaoFinanceira> Movimentacao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                var fileName = Directory.GetCurrentDirectory() + $"/../EvianAPI/appsettings.{environmentName}.json";

                var configuration = new ConfigurationBuilder().AddJsonFile(fileName).Build();
                var connectionString = configuration.GetConnectionString("App");

                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public ApplicationDbContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public async new Task<int> SaveChanges()
        {
            var recordSaved = await base.SaveChangesAsync();

            return recordSaved;
        }
    }
}
