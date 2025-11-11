using Dominio.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Service
{
    public interface ITransacaoService
    {
        Task<IEnumerable<TransacaoResponseDto>> GetAllAsync();
        Task<TransacaoResponseDto?> GetByIdAsync(int id);
        Task<TransacaoResponseDto> CreateAsync(TransacaoCreateDto dto);
        Task UpdateAsync(TransacaoUpdateDto dto);
        Task DeleteAsync(int id);

        Task<IEnumerable<TransacaoResponseDto>> GetByPeriodoAsync(DateTime start, DateTime end);
        Task<IEnumerable<ResumoCategoriaDto>> GetResumoPorCategoriaAsync(DateTime start, DateTime end);
        Task<IEnumerable<ResumoMensalDto>> GetResumoMensalAsync(int ano);
    }
}
