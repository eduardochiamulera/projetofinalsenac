using AutoMapper;
using Evian.Entities.Entities;
using Evian.Entities.Entities.DTO;
using Evian.Helpers;
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
    public class PessoaController : ApiEmpresaBaseController<Pessoa, PessoaBL>
    {
        public PessoaController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = All().AsQueryable();
            var result = _mapper.Map<List<PessoaDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("fornecedores")]
        public IActionResult GetFornecedores()
        {
            var entities = All().Where(x => x.Fornecedor).AsQueryable();
            var result = _mapper.Map<List<PessoaDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("clientes")]
        public IActionResult GetClientes()
        {
            var entities = All().Where(x => x.Cliente).AsQueryable();
            var result = _mapper.Map<List<PessoaDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("pageNo/{pageNo}/{ehCliente}")]
        public IActionResult Get(int pageNo, bool ehCliente)
        {
            var skipRecords = (pageNo - 1) * PAGE_SIZE;
            var entities = UnitOfWork.PessoaBL.BuscaPessoasPorTipo(ehCliente, skipRecords, PAGE_SIZE);

            var countRegister = entities.Count();
            var result = _mapper.Map<List<PessoaDTO>>(entities);
            return Ok(new PagedResult<PessoaDTO>(result, pageNo, PAGE_SIZE, countRegister));
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {
            var entity = UnitOfWork.PessoaBL.AllIncluding(x => x.Pais, x => x.Cidade, x => x.Estado).FirstOrDefault(x => x.Id == key);

            if (entity is null)
            {
                throw new Exception("Registro não encontrado ou já excluído");
            }
            else
            {
                var result = _mapper.Map<PessoaDTO>(entity);
                return Ok(result);
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]PessoaDTO model)
        {
            if (model == null)
                return BadRequest(ModelState);

            var entity = _mapper.Map<Pessoa>(model);

            ModelState.Clear();

            Insert(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            await UnitSave();

            model = _mapper.Map<PessoaDTO>(entity);

            return Created("", model);
        }

        [HttpPut("{key}")]
        public virtual async Task<IActionResult> Put(Guid key, [FromBody] PessoaDTO model)
        {
            if (model == null || key == default(Guid) || key == null)
                return BadRequest(ModelState);

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");

            entity = _mapper.Map<PessoaDTO, Pessoa>(model, entity);
            
            ModelState.Clear();
            Update(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            model = _mapper.Map<Pessoa, PessoaDTO>(entity, model);

            return Ok(model);
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
        public virtual async Task<IActionResult> Patch(Guid key, [FromBody] JsonPatchDocument<PessoaDTO> model)
        {
            if (model == null || key == default(Guid) || key == null)
                return BadRequest(ModelState);

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");
            
            var pessoaDTO = new PessoaDTO();
            
            pessoaDTO = _mapper.Map<PessoaDTO>(entity);

            model.ApplyTo(pessoaDTO);
            
            entity = _mapper.Map<PessoaDTO, Pessoa>(pessoaDTO, entity);

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

            var result = _mapper.Map<PessoaDTO>(entity);

            return Ok(result);
        }
    }
}
