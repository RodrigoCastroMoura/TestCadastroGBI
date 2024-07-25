using AutoMapper;
using FluentValidation.Results;
using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;
using GbiTestCadastro.Domain.Validations.Usuarios;
using GbiTestCadastro.Dto.Usuarios;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Create
{
    public class UsuarioCreateUsecases : IUsuarioCreateUsecases
    {
        private readonly IUsuarioRepository iUsuarioRepository;
        private readonly IMapper mapper;
        private UsuarioCreateValidation usuarioValidation;

        public UsuarioCreateUsecases(IUsuarioRepository iUsuarioRepository, IMapper mapper)
        {
            this.iUsuarioRepository = iUsuarioRepository;
            this.mapper = mapper;
            usuarioValidation = new UsuarioCreateValidation();
        }

        public async Task<ServiceResponse<Usuario>> Execute(UsuarioCreateDto dto)
        {
            var usuario = mapper.Map<Usuario>(dto);

            usuarioValidation.Validate(usuario);

            ValidationResult resultadoValidacao = usuarioValidation.Validate(usuario);

            var response = new ServiceResponse<Usuario>();

            if (resultadoValidacao.IsValid)
            {          
                try
                {
                    usuario.Data = DateTime.Now;
                    await iUsuarioRepository.Add(usuario);
                    response.Data = usuario;
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }

                return response;             
            }
            else
            {
                response.Success = false;
                response.Message = resultadoValidacao.Errors[0].ErrorMessage;

                return response;
            }

           

        }
    }
}
