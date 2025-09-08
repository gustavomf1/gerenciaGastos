using AutoMapper;
using Dominio.Dtos;
using Dominio.Entidades;
using Interface.Repositorio;
using Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UsuarioService : IUsuarioService
    {
        private IUsuarioRepositorio repositorio;
        private IMapper mapper;

        public UsuarioService(IUsuarioRepositorio repositorio, IMapper mapper)
        {
            this.repositorio = repositorio;
            this.mapper = mapper;
        }

        public async Task<UsuarioDto> addAsync(UsuarioDto usuario)
        {
            var entidade = mapper.Map<Usuario>(usuario);
            entidade = await this.repositorio.addAsync(entidade);
            return mapper.Map<UsuarioDto>(entidade);

        }

        public async Task<IEnumerable<UsuarioDto>> getAllAsync(Expression<Func<Usuario, bool>> expression)
        {
            var listaCat =
                await this.repositorio.getAllAsync(expression);
            return mapper.Map<IEnumerable<UsuarioDto>>(listaCat);
        }

        public async Task<UsuarioDto?> getAsync(int id)
        {
            var cat = await this.repositorio.getAsync(id);
            return mapper.Map<UsuarioDto>(cat);
        }

        public async Task removeAsync(int id)
        {
            var cat = await this.repositorio.getAsync(id);
            if (cat != null)
                await this.repositorio.removeAsync(cat);
        }

        public async Task updateAsync(UsuarioDto Usuario)
        {
            var cat = mapper.Map<Usuario>(Usuario);
            await this.repositorio.updateAsync(cat);
        }
    }
}
