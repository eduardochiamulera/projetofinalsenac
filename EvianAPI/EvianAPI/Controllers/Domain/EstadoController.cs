using AutoMapper;
using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using EvianAPI.Controllers.Base;
using EvianBL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EvianAPI.Controllers.Domain
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : ApiDomainBaseController<Estado, EstadoBL>
    {

        public EstadoController(IMapper mapper) : base(mapper) {}

        [HttpGet("{paisId}")]
        public IActionResult Get(Guid paisId)
        {
            var entities = All().Where(x => x.PaisId == paisId).AsQueryable();
            var result = _mapper.Map<List<EstadoDTO>>(entities);
            return Ok(result);
        }

    }
}
