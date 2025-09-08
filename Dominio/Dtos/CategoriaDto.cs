using Dominio.Entidades;
using Dominio.Enum;
using System.Text.Json.Serialization;

namespace Dominio.Dtos
{
    public class CategoriaDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        [JsonIgnore]
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
