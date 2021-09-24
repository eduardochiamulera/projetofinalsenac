using Evian.Entities.Base;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Evian.Entities
{
    public class Pais : DomainBase
    {
        public string Nome { get; set; }
        public string CodigoIbge { get; set; }
        public string CodigoBacen { get; set; }
        public string CodigoSiscomex { get; set; }
        public string Sigla { get; set; }
    }
}