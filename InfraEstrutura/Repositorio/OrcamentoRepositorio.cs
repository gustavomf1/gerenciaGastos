using Dominio.Entidades;
using InfraEstrutura.Data;
using Interface.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfraEstrutura.Repositorio
{
    public class OrcamentoRepositorio : IOrcamentoRepositorio
    {
        private GastosContexto contexto;

        public OrcamentoRepositorio(GastosContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Orcamento> addAsync(Orcamento orcamento)
        {
            await this.contexto.Orcamentos.AddAsync(orcamento);
            await this.contexto.SaveChangesAsync();
            return orcamento;

        }

        public async Task<IEnumerable<Orcamento>> getAllAsync()
        {
            return await this.contexto.Orcamentos
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<Orcamento?> getAsync(int id)
        {
            return await
                this.contexto.Orcamentos.FindAsync(id);
        }

        public async Task removeAsync(Orcamento orcamento)
        {
            this.contexto.Orcamentos.Remove(orcamento);
            await this.contexto.SaveChangesAsync();

        }

        public async Task updateAsync(Orcamento orcamento)
        {
            this.contexto.Entry(orcamento).State
                = EntityState.Modified;
            await this.contexto.SaveChangesAsync();
        }
    }
}
