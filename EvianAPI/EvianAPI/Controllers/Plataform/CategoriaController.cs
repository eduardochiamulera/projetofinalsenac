using AutoMapper;
using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using Evian.Entities.Enums;
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
    public class CategoriaController : ApiEmpresaBaseController<Categoria, CategoriaBL>
    {
        public CategoriaController(IMapper mapper) : base(mapper) { }

        [HttpGet("")]
        public IActionResult Get()
        {
            var entities = All().AsQueryable();
            var result = _mapper.Map<List<CategoriaDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("{tipoCarteira}")]
        public IActionResult GetByTipoCarteira(string tipoCarteira)
        {
            var tipoCarteiraEnum = (TipoCarteira)Enum.Parse(typeof(TipoCarteira), tipoCarteira);

            var entities = All().Where(x => x.TipoCarteira == tipoCarteiraEnum).AsQueryable();
            var result = _mapper.Map<List<CategoriaDTO>>(entities);
            return Ok(result);
        }
    }
}
