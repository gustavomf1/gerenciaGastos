using Dominio.Entidades;
using System.Linq.Expressions;


namespace Interface.Repositorio
{
    public interface IOrcamentoRepositorio
    {
        Task<Orcamento> addAsync(Orcamento orcamento);
        Task removeAsync(Orcamento orcamento);
        Task<Orcamento?> getAsync(int id);
        Task<IEnumerable<Orcamento>> getAllAsync();
        Task updateAsync(Orcamento orcamento);
    }
}
