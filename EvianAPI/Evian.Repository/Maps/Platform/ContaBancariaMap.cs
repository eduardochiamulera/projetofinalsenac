using Evian.Entities.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class ContaBancariaMap : EmpresaBaseMap<ContaBancaria> 
    {
        public ContaBancariaMap() : base("conta_bancaria"){}

        public override void Configure(EntityTypeBuilder<ContaBancaria> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.NomeConta).HasColumnName("nome_conta").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Agencia).HasColumnName("agencia").IsRequired();
            builder.Property(x => x.DigitoAgencia).HasColumnName("digito_agencia");
            builder.Property(x => x.Conta).HasColumnName("conta").IsRequired();
            builder.Property(x => x.DigitoConta).HasColumnName("digito_conta");
            builder.Property(x => x.ValorInicial).HasColumnName("valor_inicial");

            builder.Property(x => x.BancoId).HasColumnName("banco_id").HasMaxLength(36);
            builder.HasOne(x => x.Banco).WithMany().HasForeignKey(x => x.BancoId);

            builder.HasOne(x => x.SaldoHistorico).WithOne(x => x.ContaBancaria).HasForeignKey<SaldoHistorico>(x => x.ContaBancariaId);
        }
    }
}
