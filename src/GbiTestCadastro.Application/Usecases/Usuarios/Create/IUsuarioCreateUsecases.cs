using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Dto.Usuarios;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Create
{
    public interface IUsuarioCreateUsecases
    {
        Task<ServiceResponse<Usuario>> Execute(UsuarioCreateDto dto);
    }
}
