using Dominio.Dtos;
using Dominio.Entidades;
using System.Linq.Expressions;


namespace Interface.Service
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> addAsync(CategoriaDto categoria);
        Task removeAsync(int id);
        Task<CategoriaDto?> getAsync(int id);
        Task<IEnumerable<CategoriaDto>>
            getAllAsync(Expression<Func<Categoria, bool>>
                        expression);
        Task updateAsync(CategoriaDto categoria);
    }
}
