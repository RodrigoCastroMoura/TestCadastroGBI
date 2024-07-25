using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Dto.Usuarios;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Update
{
    public  interface IUsuarioUpdateUsecases
    {
        Task<ServiceResponse<Usuario>> Execute(UsuarioUpdateDto dto);
    }
}
