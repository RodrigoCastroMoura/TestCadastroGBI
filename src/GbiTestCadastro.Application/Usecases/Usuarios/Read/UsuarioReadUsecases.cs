using AutoMapper;
using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Dto.Usuarios;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Read
{
    public class UsuarioReadUsecases : IUsuarioReadUsecases
    {
        private readonly IUsuarioRepository iUsuarioRepository;
        private readonly IMapper mapper;

        public UsuarioReadUsecases(IUsuarioRepository iUsuarioRepository, IMapper mapper)
        {
            this.iUsuarioRepository = iUsuarioRepository;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<UsuarioDto>> Execute(int id)
        {
            var response = new ServiceResponse<UsuarioDto>();

            try
            {
                var usuario = await iUsuarioRepository.Get(id);

                response.Data = mapper.Map<UsuarioDto>(usuario);
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
           
        }
        public async Task<ServiceResponse<PagedResponse<UsuarioDto>>> Execute()
        {
            var response = new ServiceResponse<PagedResponse<UsuarioDto>>();

            try
            {
                var usuarios = await iUsuarioRepository.GetAll();
                var usuariosDto = mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
                var pagedResponse = new PagedResponse<UsuarioDto>(usuariosDto, usuariosDto.Count());
                response.Data = pagedResponse;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }
        }

    }
}
