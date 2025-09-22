using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class TransacaoResponseDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Tipo { get; set; }

        public string Categoria { get; set; }
        public string Usuario { get; set; }
        public string? Orcamento { get; set; }
    }
}
