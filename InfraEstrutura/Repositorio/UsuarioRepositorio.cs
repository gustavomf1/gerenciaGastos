using Dominio.Entidades;
using InfraEstrutura.Data;
using Interface.Repositorio;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfraEstrutura.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private GastosContexto contexto;

        public UsuarioRepositorio(GastosContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task<Usuario> addAsync(Usuario Usuario)
        {
            await this.contexto.Usuarios.AddAsync(Usuario);
            await this.contexto.SaveChangesAsync();
            return Usuario;

        }

        public async Task<IEnumerable<Usuario>> getAllAsync(Expression<Func<Usuario, bool>> expression)
        {
            return await
                 this.contexto.Usuarios
                 .Where(expression)
                 .OrderBy(p => p.Nome)
                 .ToListAsync();
        }

        public async Task<Usuario?> getAsync(int id)
        {
            return await
                this.contexto.Usuarios.FindAsync(id);
        }

        public async Task removeAsync(Usuario Usuario)
        {
            this.contexto.Usuarios.Remove(Usuario);
            await this.contexto.SaveChangesAsync();

        }

        public async Task updateAsync(Usuario Usuario)
        {
            this.contexto.Entry(Usuario).State
                = EntityState.Modified;
            await this.contexto.SaveChangesAsync();
        }
    }
}
