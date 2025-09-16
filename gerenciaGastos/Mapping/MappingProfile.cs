using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;
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
        }
    }
}
