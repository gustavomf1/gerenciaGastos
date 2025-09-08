using Dominio.Dtos;
using Interface.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gerenciaGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService service;

        public UsuarioController(IUsuarioService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioDto>>
            addAsync(UsuarioDto UsuarioDto)
        {

            var dto = await this.service.addAsync(UsuarioDto);
            return Ok(dto);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>>
            getAllAsync()
        {
            var lista = await this.service.getAllAsync(p => true);
            return Ok(lista);

        }


        [HttpGet("/filtrar/{nome}")]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>>
           getNomeAsync(string nome)
        {
            var lista = await this.service.getAllAsync(
                p => p.Nome.Contains(nome));
            return Ok(lista);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>>
            getAsync(int id)
        {
            var usu = await this.service.getAsync(id);
            if (usu == null)
                return NotFound(); //não encontrou
            else
                return Ok(usu);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteAsync(int id)
        {

            await this.service.removeAsync(id);
            return NoContent(); //sem retorno
        }

        [HttpPut]
        public async Task<ActionResult>
            updateAsync(UsuarioDto usu)
        {
            await this.service.updateAsync(usu);
            return NoContent();

        }
    }
}
