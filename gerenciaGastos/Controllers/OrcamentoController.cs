using Dominio.Dtos;
using FluentValidation;
using Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gerenciaGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrcamentoController : ControllerBase
    {
        private readonly IOrcamentoService service;
        private readonly IValidator<OrcamentoDto> validator;

        public OrcamentoController(IOrcamentoService service, IValidator<OrcamentoDto> validator)
        {
            this.service = service;
            this.validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoDto>> AddAsync(OrcamentoDto orcamento)
        {
            var result = await validator.ValidateAsync(orcamento);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var dto = await service.addAsync(orcamento);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentoDto>>> GetAllAsync()
        {
            var lista = await service.getAllAsync();
            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrcamentoDto>> GetAsync(int id)
        {
            var orc = await service.getAsync(id);
            if (orc == null)
                return NotFound();

            return Ok(orc);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await service.removeAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(OrcamentoDto orcamento)
        {
            var result = await validator.ValidateAsync(orcamento);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await service.updateAsync(orcamento);
            return NoContent();
        }
    }
}
