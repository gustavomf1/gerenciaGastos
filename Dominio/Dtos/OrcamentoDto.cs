using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class OrcamentoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int UsuarioId { get; set; }

        public decimal ValorLimite { get; set; }
        public decimal ValorAtual { get; set; }

        public int Mes { get; set; }
        public int Ano { get; set; }

        [JsonIgnore]
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}
