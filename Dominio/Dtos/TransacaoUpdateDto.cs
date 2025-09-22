using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class TransacaoUpdateDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public int Tipo { get; set; }

        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public int? OrcamentoId { get; set; }
    }
}
