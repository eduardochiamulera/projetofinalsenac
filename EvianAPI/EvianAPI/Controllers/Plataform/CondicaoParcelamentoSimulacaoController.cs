using AutoMapper;
using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using EvianAPI.Controllers.Base;
using EvianBL;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPatchAttribute = Microsoft.AspNetCore.Mvc.HttpPatchAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EvianAPI.Controllers.Platform
{
    [ApiController]
    [Route("api/[controller]")]
    public class CondicaoParcelamentoSimulacaoController : ApiEmpresaBaseController<CondicaoParcelamento, CondicaoParcelamentoBL>
    {
        public CondicaoParcelamentoSimulacaoController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        public IActionResult Get([FromQuery]Guid condicaoParcelamentoId, DateTime dataReferencia, decimal valor)
        {
            return Ok(UnitOfWork.CondicaoParcelamentoBL.GetPrestacoes(condicaoParcelamentoId, dataReferencia, valor));
        }
    }
}
