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
    public class ConciliacaoBancariaController : ApiEmpresaBaseController<ConciliacaoBancaria, ConciliacaoBancariaBL>
    {
        public ConciliacaoBancariaController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = All().AsQueryable();
            var result = _mapper.Map<List<ConciliacaoBancariaDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            if (!All().Any(x => x.Id == key))
            {
                throw new Exception("Registro não encontrado ou já excluído");
            }
            else
            {
                return Ok(SingleResult.Create(All().Where(x => x.Id == key)));
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]ConciliacaoBancariaDTO model)
        {
            if (model == null)
                return BadRequest(ModelState);

            var entity = _mapper.Map<ConciliacaoBancaria>(model);

            ModelState.Clear();

            Insert(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            await UnitSave();

            model = _mapper.Map<ConciliacaoBancariaDTO>(entity);

            return Created("", model);
        }

        [HttpPut("{key}")]
        public virtual async Task<IActionResult> Put(Guid key, [FromBody] ConciliacaoBancariaDTO model)
        {
            if (model == null || key == default(Guid) || key == null)
                return BadRequest(ModelState);

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");

            entity = _mapper.Map<ConciliacaoBancariaDTO, ConciliacaoBancaria>(model, entity);
            
            ModelState.Clear();
            Update(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            //try
            //{
            //    await UnitSave();

            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!Exists(key))
            //        return NotFound();
            //    else
            //        throw;
            //}

            return Ok();
        }

        [HttpDelete("{key}")]
        public virtual async Task<IActionResult> Delete(Guid key)
        {
            if (key == default(Guid) || key == null)
                return BadRequest();

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");

            ModelState.Clear();
            Delete(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            await UnitSave();

            return StatusCode(((int)HttpStatusCode.NoContent));
        }

        [HttpPatch("{key}")]
        public virtual async Task<IActionResult> Patch(Guid key, [FromBody] JsonPatchDocument<ConciliacaoBancariaDTO> model)
        {
            if (model == null || key == default(Guid) || key == null)
                return BadRequest(ModelState);

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");
            
            var ConciliacaoBancariaDTO = new ConciliacaoBancariaDTO();
            
            ConciliacaoBancariaDTO = _mapper.Map<ConciliacaoBancariaDTO>(entity);

            model.ApplyTo(ConciliacaoBancariaDTO);
            
            entity = _mapper.Map<ConciliacaoBancariaDTO, ConciliacaoBancaria>(ConciliacaoBancariaDTO, entity);

            ModelState.Clear();

            Update(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            try
            {
                await UnitSave();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(key))
                    return NotFound();
                else
                    throw;
            }

            var result = _mapper.Map<ConciliacaoBancariaDTO>(entity);

            return Ok(result);
        }
    }
}
