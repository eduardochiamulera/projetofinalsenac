using Evian.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Evian.Entities.Entities.Base
{
    public class EmpresaBase : DomainBase
    {
        [Required]
        public Guid EmpresaId { get; set; }
    }
}
