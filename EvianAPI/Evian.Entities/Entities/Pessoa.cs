using Evian.Entities.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Evian.Entities.Entities
{
    public class Pessoa : EmpresaBase
    {
        public string Nome { get; set; }

        public string TipoDocumento { get; set; }

        public string CPFCNPJ { get; set; }

        public string CEP { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public Guid? CidadeId { get; set; }

        public Guid? EstadoId { get; set; }

        public Guid? PaisId { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Contato { get; set; }

        public string Observacao { get; set; }

        public string Email { get; set; }

        public string NomeComercial { get; set; }
        
        [NotMapped]
        public string EstadoName { get; set; }

        [NotMapped]
        public string PaisName { get; set; }

        [NotMapped]
        public string CidadeName { get; set; }

        public bool Cliente { get; set; }

        public bool Fornecedor { get; set; }

        public virtual Pais Cidade { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual Pais Pais { get; set; }
    }
}