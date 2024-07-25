using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Dto.Usuarios;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Read
{
    public interface IUsuarioReadUsecases
    {
        Task<ServiceResponse<UsuarioDto>> Execute(int id);

        Task<ServiceResponse<PagedResponse<UsuarioDto>>> Execute();
    }
}
