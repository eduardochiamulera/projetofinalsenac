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
    public class CidadeController : ApiDomainBaseController<Cidade, CidadeBL>
    {
        public CidadeController(IMapper mapper) : base(mapper){}

        [HttpGet]
        public IActionResult Get()
        {
            //var t = _ma
                //All().AsQueryable()
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(string nome)
        {
            var cidade = _mapper.Map<CidadeDTO>(UnitOfWork.CidadeBL.FindByNome(nome));
            
            return Ok(cidade);
        }

    }
}
