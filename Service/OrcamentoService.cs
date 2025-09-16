using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;
using Interface.Repositorio;
using Interface.Service;

namespace Service
{
    public class OrcamentoService : IOrcamentoService
    {
        private IMapper mapper;
        private IOrcamentoRepositorio repositorio;

        public OrcamentoService(IMapper mapper, IOrcamentoRepositorio repositorio)
        {
            this.mapper = mapper;
            this.repositorio = repositorio;
        }

        public async Task<OrcamentoDto> addAsync(OrcamentoDto orcamento)
        {
            var entidade = mapper.Map<Orcamento>(orcamento);
            entidade.ValorAtual = entidade.ValorLimite;
            entidade = await this.repositorio.addAsync(entidade);
            return mapper.Map<OrcamentoDto>(entidade);

        }

        public async Task<IEnumerable<OrcamentoDto>> getAllAsync()
        {
            var listaOrcamentos = await this.repositorio.getAllAsync();
            return mapper.Map<IEnumerable<OrcamentoDto>>(listaOrcamentos);
        }

        public async Task<OrcamentoDto?> getAsync(int id)
        {
            var orc = await this.repositorio.getAsync(id);
            return mapper.Map<OrcamentoDto>(orc);
        }

        public async Task removeAsync(int id)
        {
            var orc = await this.repositorio.getAsync(id);
            if (orc != null)
                await this.repositorio.removeAsync(orc);
        }

        public async Task updateAsync(OrcamentoDto orcamento)
        {
            var orc = mapper.Map<Orcamento>(orcamento);
            await this.repositorio.updateAsync(orc);
        }
    }
}
