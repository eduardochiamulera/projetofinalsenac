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
    public class CidadeController : ApiDomainBaseController<Cidade, CidadeBL>
    {
        public CidadeController(IMapper mapper) : base(mapper){}

        [HttpGet("{estadoId}")]
        public IActionResult Get(Guid estadoId)
        {
            var entities = All().Where(x => x.EstadoId == estadoId).AsQueryable();
            var result = _mapper.Map<List<CidadeDTO>>(entities);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(string nome)
        {
            var cidade = _mapper.Map<CidadeDTO>(UnitOfWork.CidadeBL.FindByNome(nome));
            
            return Ok(cidade);
        }

    }
}
