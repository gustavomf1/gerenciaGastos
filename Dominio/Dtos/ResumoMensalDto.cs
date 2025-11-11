using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Dtos
{
    public class ResumoMensalDto
    {
        public int Mes { get; set; }    // 1..12
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
    }
}
