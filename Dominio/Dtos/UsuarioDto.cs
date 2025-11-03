using Dominio.Entidades;
using System.Text.Json.Serialization;

namespace Dominio.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;

        [JsonIgnore]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        [JsonIgnore]
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
        [JsonIgnore]
        public ICollection<Orcamento> Orcamentos { get; set; } = new List<Orcamento>();
    }
}
