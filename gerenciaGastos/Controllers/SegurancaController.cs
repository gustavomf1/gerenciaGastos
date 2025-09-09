using Dominio.Dtos;
using Dominio.Entidades;
using InfraEstrutura.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace gerenciaGastos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegurancaController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly GastosContexto _context;

        public SegurancaController(IConfiguration config, GastosContexto context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDetalhes)
        {
            var usuario = await ValidarUsuario(loginDetalhes);
            if (usuario != null)
            {
                var tokenString = GerarTokenJWT(usuario);
                return Ok(new
                {
                    access_token = tokenString,
                    token_type = "Bearer",
                    expires_in = 60 * 60 // 60 min
                });
            }
            else
            {
                return Unauthorized("Usuário ou senha inválidos");
            }
        }

        private string GerarTokenJWT(Usuario usuario)
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<Usuario?> ValidarUsuario(LoginDto loginDetalhes)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginDetalhes.Email);

            if (usuario == null)
                return null;

            if (usuario.SenhaHash == loginDetalhes.SenhaHash)
                return usuario;

            return null;
        }
    }
}
