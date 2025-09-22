using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;
using Dominio.Enum;
namespace gerenciaGastos.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<Categoria, CategoriaDto>()
                .ReverseMap();
            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();
            CreateMap<Orcamento, OrcamentoDto>() 
                .ReverseMap();

            CreateMap<Transacao, TransacaoResponseDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()))
                .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Descricao : string.Empty))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario != null ? src.Usuario.Nome : string.Empty))
                .ForMember(dest => dest.Orcamento, opt => opt.MapFrom(src => src.Orcamento != null ? src.Orcamento.Titulo : null));

            CreateMap<TransacaoCreateDto, Transacao>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoTransacao)src.Tipo));

            CreateMap<TransacaoUpdateDto, Transacao>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => (TipoTransacao)src.Tipo));
        }
    }
}
