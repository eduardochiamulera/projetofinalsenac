using Evian.Notifications;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Evian.Entities.Entities.Base
{
    public class DomainBase
    {
        [Key]
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataInclusao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataAlteracao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataExclusao { get; set; }

        [Required]
        public string UsuarioInclusao { get; set; }

        public string UsuarioAlteracao { get; set; }

        public string UsuarioExclusao { get; set; }

        public bool Ativo { get; set; } = true;

        #region Notification

        [NotMapped]
        [JsonIgnore]
        public Notification Notification { get; } = new Notification();

        public virtual void Validate() { }

        public void Fail(bool condition, Error error)
        {
            if (condition)
                Notification.Errors.Add(error);
        }

        public bool IsValid()
        {
            return !Notification.HasErrors;
        }

        #endregion
    }
}
