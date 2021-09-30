using Evian.Entities;
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

                public DbSet<ContaPagar> ContaPagars { get; set; }
        public DbSet<ContaReceber> ContaRecebers { get; set; }
        public DbSet<Cidade> Cidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<ContaPagar>().Ignore(m => m.ContaPagarParcelaPai);
            modelBuilder.Entity<ContaPagar>().Ignore(m => m.ContaPagarRepeticaoPai);
            modelBuilder.Entity<ContaReceber>().Ignore(m => m.ContaReceberParcelaPai);
            modelBuilder.Entity<ContaReceber>().Ignore(m => m.ContaReceberRepeticaoPai);
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
