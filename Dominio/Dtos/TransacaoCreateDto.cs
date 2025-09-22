using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class TransacaoCreateDto
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public int Tipo { get; set; } // 0 = Receita, 1 = Despesa

        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public int? OrcamentoId { get; set; }
    }
}
