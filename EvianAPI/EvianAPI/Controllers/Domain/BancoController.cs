using AutoMapper;
using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using EvianAPI.Controllers.Base;
using EvianBL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EvianAPI.Controllers.Domain
{
    [ApiController]
    [Route("api/[controller]")]
    public class BancoController : ApiDomainBaseController<Banco, BancoBL>
    {
        public BancoController(IMapper mapper) : base(mapper) { }        

        [HttpGet]
        public IActionResult Get()
        {
            var entities = _mapper.Map<BancoDTO>(All());
            return Ok(entities);
        }

    }
}
