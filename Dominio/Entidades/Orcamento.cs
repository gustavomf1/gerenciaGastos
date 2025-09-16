using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Orcamento
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public decimal ValorLimite { get; set; }
        public decimal ValorAtual { get; set; }

        public int Mes { get; set; } 
        public int Ano { get; set; }

        public ICollection<Transacao> Transacoes { get; set; }
    }
}
