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
        private readonly IOrcamentoService _service;
        private readonly IValidator<OrcamentoDto> _validator;

        public OrcamentoController(IOrcamentoService service, IValidator<OrcamentoDto> validator)
        {
            _service = service;
            _validator = validator;
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoDto>> AddAsync(OrcamentoDto orcamento)
        {
            var result = await _validator.ValidateAsync(orcamento);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var dto = await _service.addAsync(orcamento);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrcamentoDto>>> GetAllAsync()
        {
            var lista = await _service.getAllAsync();
            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrcamentoDto>> GetAsync(int id)
        {
            var orc = await _service.getAsync(id);
            if (orc == null)
                return NotFound();

            return Ok(orc);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.removeAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(OrcamentoDto orcamento)
        {
            var result = await _validator.ValidateAsync(orcamento);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            await _service.updateAsync(orcamento);
            return NoContent();
        }
    }
}
