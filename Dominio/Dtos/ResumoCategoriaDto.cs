using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class ResumoCategoriaDto
    {
        public string Categoria { get; set; }
        public decimal Total { get; set; }
        public decimal TotalAbsoluto => Math.Abs(Total);
        public int Count { get; set; }
    }
}
