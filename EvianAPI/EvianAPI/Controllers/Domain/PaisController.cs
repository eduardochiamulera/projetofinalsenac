using AutoMapper;
using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using EvianAPI.Controllers.Base;
using EvianBL;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EvianAPI.Controllers.Domain
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaisController : ApiDomainBaseController<Pais, PaisBL>
    {
        public PaisController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        public IActionResult Get()
        {
            var paises = All().OrderBy(x => x.Nome);
            var result = _mapper.Map<List<PaisDTO>>(paises);
            return Ok(result);
        }

    }
}
