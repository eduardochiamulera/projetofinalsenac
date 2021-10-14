using Newtonsoft.Json;
using System;


namespace Evian.Entities.Entities.DTO
{
    public abstract class BaseDTO
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}
