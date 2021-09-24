using Evian.Entities;
using Evian.Repository.Maps.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evian.Repository.Maps.Platform
{
    public class PessoaMap : EmpresaBaseMap<Pessoa>
    {
        public PessoaMap() : base("pessoa"){}

        public override void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome).HasColumnName("nome").HasMaxLength(200).IsRequired();
            builder.Property(x => x.TipoDocumento).HasColumnName("tipo_documento").HasMaxLength(1);
            builder.Property(x => x.CPFCNPJ).HasColumnName("cpf_cnpj").HasMaxLength(14);
            builder.Property(x => x.CEP).HasColumnName("cep").HasMaxLength(8);
            builder.Property(x => x.Endereco).HasColumnName("endereco").HasMaxLength(200);
            builder.Property(x => x.Numero).HasColumnName("numero").HasMaxLength(10);
            builder.Property(x => x.Complemento).HasColumnName("complemento").HasMaxLength(200);
            builder.Property(x => x.Bairro).HasColumnName("bairro").HasMaxLength(200);
            builder.Property(x => x.Telefone).HasColumnName("telefone").HasMaxLength(10);
            builder.Property(x => x.Celular).HasColumnName("celular").HasMaxLength(11);
            builder.Property(x => x.Contato).HasColumnName("contato").HasMaxLength(100);
            builder.Property(x => x.Observacao).HasColumnName("observacao").HasMaxLength(500);
            builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(50);
            builder.Property(x => x.NomeComercial).HasColumnName("nome_comercial").HasMaxLength(100);
            builder.Property(x => x.Cliente).HasColumnName("eh_cliente").IsRequired();
            builder.Property(x => x.Fornecedor).HasColumnName("eh_fornecedor").IsRequired();

            builder.Property(x => x.CidadeId).HasColumnName("cidade_id").HasMaxLength(36);
            builder.HasOne(x => x.Cidade).WithMany().HasForeignKey(x => x.CidadeId);

            builder.Property(x => x.EstadoId).HasColumnName("estado_id").HasMaxLength(36);
            builder.HasOne(x => x.Estado).WithMany().HasForeignKey(x => x.EstadoId);

            builder.Property(x => x.PaisId).HasColumnName("pais_id").HasMaxLength(36);
            builder.HasOne(x => x.Pais).WithMany().HasForeignKey(x => x.PaisId);
        }
    }
}
