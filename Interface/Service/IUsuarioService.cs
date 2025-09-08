
using Dominio.Dtos;
using Dominio.Entidades;
using System.Linq.Expressions;

namespace Interface.Service
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> addAsync(UsuarioDto usuario);
        Task removeAsync(int id);
        Task<UsuarioDto?> getAsync(int id);
        Task<IEnumerable<UsuarioDto>>
            getAllAsync(Expression<Func<Usuario, bool>>
                        expression);
        Task updateAsync(UsuarioDto usuario);
    }
}
