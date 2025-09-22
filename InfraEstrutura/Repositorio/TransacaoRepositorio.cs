using Dominio.Entidades;
using Dominio.Enum;
using InfraEstrutura.Data;
using Interface.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Repositorio
{
    public class TransacaoRepositorio:ITransacaoRepositorio
    {
        private GastosContexto contexto;

        public TransacaoRepositorio(GastosContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Transacao> GetByIdAsync(int id)
        {
            return await contexto.Transacoes
                .Include(t => t.Categoria)
                .Include(t => t.Usuario)
                .Include(t => t.Orcamento)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Transacao>> GetAllAsync()
        {
            return await contexto.Transacoes
                .Include(t => t.Categoria)
                .Include(t => t.Usuario)
                .Include(t => t.Orcamento)
                .ToListAsync();
        }

        public async Task AddAsync(Transacao transacao)
        {
            await contexto.Transacoes.AddAsync(transacao);

            // Salva TODAS as alterações rastreadas (a nova transação E o orçamento modificado pelo Service)
            await contexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Transacao transacao)
        {
            contexto.Transacoes.Update(transacao);
            await contexto.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var transacao = await contexto.Transacoes.FindAsync(id);
            if (transacao != null)
            {
                contexto.Transacoes.Remove(transacao);
                await contexto.SaveChangesAsync();
            }
        }
    }
}
