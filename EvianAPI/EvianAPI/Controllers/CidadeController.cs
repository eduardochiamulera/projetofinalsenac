using Evian.Entities;
using EvianBL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EvianAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : ApiDomainController<Cidade, CidadeBL>
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(All().AsQueryable());
        }

    }
}
