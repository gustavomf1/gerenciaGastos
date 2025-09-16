using Dominio.Dtos;
using Dominio.Entidades;
using System.Linq.Expressions;


namespace Interface.Service
{
    public interface IOrcamentoService
    {
        Task<OrcamentoDto> addAsync(OrcamentoDto orcamento);
        Task removeAsync(int id);
        Task<OrcamentoDto?> getAsync(int id);
        Task<IEnumerable<OrcamentoDto>> getAllAsync();
        Task updateAsync(OrcamentoDto orcamento);
    }
}
