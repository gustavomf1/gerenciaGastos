using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Enum;
using Interface.Repositorio;
using Interface.Service;

namespace Service
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepositorio _repositorio;
        private readonly IOrcamentoRepositorio _orcamentoRepositorio;
        private readonly IMapper _mapper;

        public TransacaoService(ITransacaoRepositorio repositorio, IOrcamentoRepositorio orcamentoRepositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _orcamentoRepositorio = orcamentoRepositorio;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransacaoResponseDto>> GetAllAsync()
        {
            var transacoes = await _repositorio.GetAllAsync();
            return _mapper.Map<IEnumerable<TransacaoResponseDto>>(transacoes);
        }

        public async Task<TransacaoResponseDto?> GetByIdAsync(int id)
        {
            var transacao = await _repositorio.GetByIdAsync(id);
            return transacao == null ? null : _mapper.Map<TransacaoResponseDto>(transacao);
        }

        public async Task<TransacaoResponseDto> CreateAsync(TransacaoCreateDto dto)
        {
            var transacao = _mapper.Map<Transacao>(dto);

            if (transacao.OrcamentoId.HasValue)
            {
                var orcamento = await _orcamentoRepositorio.getAsync(transacao.OrcamentoId.Value);
                if (orcamento != null)
                {
                    AplicarTransacaoAoOrcamento(orcamento, transacao);
                }
            }

            await _repositorio.AddAsync(transacao);

            return _mapper.Map<TransacaoResponseDto>(transacao);
        }

        public async Task UpdateAsync(TransacaoUpdateDto dto)
        {
            var transacaoOriginal = await _repositorio.GetByIdAsync(dto.Id);
            if (transacaoOriginal == null)
            {
                throw new KeyNotFoundException("Transação não encontrada");
            }

            if (transacaoOriginal.OrcamentoId.HasValue)
            {
                var orcamento = await _orcamentoRepositorio.getAsync(transacaoOriginal.OrcamentoId.Value);
                if (orcamento != null)
                {
                    AplicarTransacaoAoOrcamento(orcamento, transacaoOriginal, desfazer: true);
                    _mapper.Map(dto, transacaoOriginal);

                    AplicarTransacaoAoOrcamento(orcamento, transacaoOriginal);
                }
            }
            else
            {
                _mapper.Map(dto, transacaoOriginal);
            }
            await _repositorio.UpdateAsync(transacaoOriginal);
        }

        public async Task DeleteAsync(int id)
        {
            var transacao = await _repositorio.GetByIdAsync(id);
            if (transacao == null) return;

            if (transacao.OrcamentoId.HasValue)
            {
                var orcamento = await _orcamentoRepositorio.getAsync(transacao.OrcamentoId.Value);
                if (orcamento != null)
                {
                    AplicarTransacaoAoOrcamento(orcamento, transacao, desfazer: true);
                }
            }
            await _repositorio.DeleteAsync(id);
        }
        private void AplicarTransacaoAoOrcamento(Orcamento orcamento, Transacao transacao, bool desfazer = false)
        {
            int multiplicador = desfazer ? -1 : 1;

            if (transacao.Tipo == TipoTransacao.Receita)
            {
                orcamento.ValorAtual += multiplicador * transacao.Valor;
            }
            else if (transacao.Tipo == TipoTransacao.Despesa)
            {
                orcamento.ValorAtual -= multiplicador * transacao.Valor;
            }
        }

        public async Task<IEnumerable<TransacaoResponseDto>> GetByPeriodoAsync(DateTime start, DateTime end)
        {
            var todas = await _repositorio.GetAllAsync();
            var filtradas = todas.Where(t => t.Data.Date >= start.Date && t.Data.Date <= end.Date);
            return _mapper.Map<IEnumerable<TransacaoResponseDto>>(filtradas);
        }

        public async Task<IEnumerable<ResumoCategoriaDto>> GetResumoPorCategoriaAsync(DateTime start, DateTime end)
        {
            var todas = await _repositorio.GetAllAsync();
            var filtradas = todas.Where(t => t.Data.Date >= start.Date && t.Data.Date <= end.Date);

            var resumo = filtradas
                 .GroupBy(t => t.Categoria != null ? t.Categoria.Descricao : "Sem categoria")
                 .Select(g => new ResumoCategoriaDto
                 {
                     Categoria = g.Key,
                     Total = g.Sum(x => x.Tipo == TipoTransacao.Receita ? x.Valor : -x.Valor),
                     Count = g.Count()
                 })
                 .OrderByDescending(r => r.Total);

            return resumo;
        }

        public async Task<IEnumerable<ResumoMensalDto>> GetResumoMensalAsync(int ano)
        {
            var todas = await _repositorio.GetAllAsync();
            var filtradas = todas.Where(t => t.Data.Year == ano);

            var mensal = Enumerable.Range(1, 12)
                .Select(m =>
                {
                    var porMes = filtradas.Where(t => t.Data.Month == m);
                    return new ResumoMensalDto
                    {
                        Mes = m,
                        TotalReceitas = porMes.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => t.Valor),
                        TotalDespesas = porMes.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => t.Valor)
                    };
                });

            return mensal;
        }
    }
}