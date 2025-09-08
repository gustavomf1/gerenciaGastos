using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Data
{
    public class ContextoGastosFactory : IDesignTimeDbContextFactory<GastosContexto>
    {
        public GastosContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GastosContexto>();


            // Defina a string de conexão de forma que o EF possa usar durante o processo de migração.
            optionsBuilder.UseSqlServer(@"Server=localhost;
                DataBase=dbGerenciaGastos;
                integrated security=true;TrustServerCertificate=True;");
            return new GastosContexto(optionsBuilder.Options);
        }
    }
}
