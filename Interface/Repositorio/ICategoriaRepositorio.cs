using Dominio.Entidades;
using System.Linq.Expressions;


namespace Interface.Repositorio
{
    public interface ICategoriaRepositorio
    {
        Task<Categoria> addAsync(Categoria categoria);
        Task removeAsync(Categoria categoria);
        Task<Categoria?> getAsync(int id);
        Task<IEnumerable<Categoria>>
            getAllAsync(Expression<Func<Categoria, bool>>
                        expression);
        Task updateAsync(Categoria categoria);
    }
}
