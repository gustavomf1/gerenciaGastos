using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<Usuario> addAsync(Usuario usuario);
        Task removeAsync(Usuario usuario);
        Task<Usuario?> getAsync(int id);
        Task<IEnumerable<Usuario>>
            getAllAsync(Expression<Func<Usuario, bool>>
                        expression);
        Task updateAsync(Usuario usuario);
    }
}
