using Dominio.Dtos;
using FluentValidation;
using Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace gerenciaGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransacaoController : ControllerBase
    {
        private ITransacaoService service;
        private IValidator<TransacaoCreateDto> validator;

        public TransacaoController(ITransacaoService service, IValidator<TransacaoCreateDto> validator)
        {
            this.service = service;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transacoes = await service.GetAllAsync();
            return Ok(transacoes);
        }

        // GET: api/transacao/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transacao = await service.GetByIdAsync(id);
            if (transacao == null)
                return NotFound();

            return Ok(transacao);
        }

        // POST: api/transacao
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransacaoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var transacao = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = transacao.Id }, transacao);
        }

        // PUT: api/transacao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransacaoUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID da transação não confere.");

            await service.UpdateAsync(dto);
            return NoContent();
        }

        // DELETE: api/transacao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("por-categoria")]
        public async Task<IActionResult> GetTotalPorCategoria(
            [FromQuery] DateTime? start = null,
            [FromQuery] DateTime? end = null)
        {
            start ??= DateTime.Now.AddMonths(-1);
            end ??= DateTime.Now;

            var dados = await service.GetResumoPorCategoriaAsync(start.Value, end.Value);
            return Ok(dados);
        }
    }
}
