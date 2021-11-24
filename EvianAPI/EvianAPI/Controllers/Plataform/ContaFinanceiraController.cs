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
    public class ContaFinanceiraController : ApiEmpresaBaseController<ContaFinanceira, ContaFinanceiraBL>
    {
        public ContaFinanceiraController(IMapper mapper) : base(mapper) { }

        [HttpGet]
        public IActionResult Get()
        {
            var entities = All().AsQueryable();
            var result = _mapper.Map<List<ContaFinanceiraDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("{mes}/{ano}")]
        public IActionResult GetContasByPeriodo(int mes, int ano)
        {
            var dataInicio = new DateTime(ano, mes, 1);
            var ultimodia = DateTime.DaysInMonth(ano, mes);
            var dataFinal = new DateTime(ano, mes, ultimodia).AddHours(23).AddMinutes(59).AddSeconds(59);

            var entities = UnitOfWork.ContaFinanceiraBL.AllIncluding(x => x.Categoria).Where(x => x.DataEmissao >= dataInicio && x.DataEmissao <= dataFinal).AsQueryable();
            var result = _mapper.Map<List<ContaFinanceiraDTO>>(entities);
            return Ok(result);
        }

        private IQueryable<ContaFinanceira> GetContasFinanceiras(TipoContaFinanceira tipo)
        {
            return UnitOfWork.ContaFinanceiraBL
                .AllIncluding(x => x.Pessoa, x => x.FormaPagamento, x => x.CondicaoParcelamento, x => x.Categoria).Where(x => x.TipoContaFinanceira == tipo).AsQueryable();
        }

        [HttpGet("contapagar")]
        public IActionResult GetContasPagar()
        {
            var entities = GetContasFinanceiras(TipoContaFinanceira.ContaPagar);
            var result = _mapper.Map<List<ContaFinanceiraDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("contareceber")]
        public IActionResult GetContasReceber()
        {
            var entities = GetContasFinanceiras(TipoContaFinanceira.ContaReceber);
            var result = _mapper.Map<List<ContaFinanceiraDTO>>(entities);
            return Ok(result);
        }

        [HttpGet("{key}")]
        public IActionResult Get(Guid key)
        {

            var entity = UnitOfWork.ContaFinanceiraBL
                .AllIncluding(x => x.Pessoa, x => x.FormaPagamento, x => x.CondicaoParcelamento, x => x.Categoria)
                .FirstOrDefault(x => x.Id == key);

            entity.Baixas = UnitOfWork.ContaFinanceiraBaixaBL.All.Where(x => x.ContaFinanceiraId == key).ToList();

            if (entity is null)
            {
                throw new Exception("Registro não encontrado ou já excluído");
            }
            else
            {
                var result = _mapper.Map<ContaFinanceiraDTO>(entity);
                return Ok(result);
            }

        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody]ContaFinanceiraDTO model)
        {
            if (model == null)
                return BadRequest(ModelState);

            var entity = _mapper.Map<ContaFinanceira>(model);

            ModelState.Clear();

            Insert(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            await UnitSave();

            model = _mapper.Map<ContaFinanceiraDTO>(entity);

            return Created("", model);
        }

        [HttpPut("{key}")]
        public virtual async Task<IActionResult> Put(Guid key, [FromBody] ContaFinanceiraDTO model)
        {
            if (model == null || key == default(Guid) || key == null)
                return BadRequest(ModelState);

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");

            entity = _mapper.Map(model, entity);
            
            ModelState.Clear();
            Update(entity);

            if (!ModelState.IsValid)
                AddErrorModelState(ModelState);

            model = _mapper.Map(entity, model);

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
        public virtual async Task<IActionResult> Patch(Guid key, [FromBody] JsonPatchDocument<ContaFinanceiraDTO> model)
        {
            if (model == null || key == default(Guid) || key == null)
                return BadRequest(ModelState);

            var entity = Find(key);

            if (entity == null || !entity.Ativo)
                throw new Exception("Registro não encontrado ou já excluído");
            
            var ContaFinanceiraDTO = new ContaFinanceiraDTO();
            
            ContaFinanceiraDTO = _mapper.Map<ContaFinanceiraDTO>(entity);

            model.ApplyTo(ContaFinanceiraDTO);
            
            entity = _mapper.Map<ContaFinanceiraDTO, ContaFinanceira>(ContaFinanceiraDTO, entity);

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

            var result = _mapper.Map<ContaFinanceiraDTO>(entity);

            return Ok(result);
        }
    }
}
