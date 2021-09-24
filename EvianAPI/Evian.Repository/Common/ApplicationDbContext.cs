using Evian.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evian.Repository.Core.Common
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ContaPagar> ContaPagars { get; set; }
        public DbSet<ContaReceber> ContaRecebers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Entity<ContaPagar>().Ignore(m => m.ContaPagarParcelaPai);
            modelBuilder.Entity<ContaPagar>().Ignore(m => m.ContaPagarRepeticaoPai);
            modelBuilder.Entity<ContaReceber>().Ignore(m => m.ContaReceberParcelaPai);
            modelBuilder.Entity<ContaReceber>().Ignore(m => m.ContaReceberRepeticaoPai);
        }

        public ApplicationDbContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }        
    }
}
