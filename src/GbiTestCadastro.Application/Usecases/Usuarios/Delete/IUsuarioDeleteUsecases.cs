using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Delete
{
    public  interface IUsuarioDeleteUsecases
    {
        Task<ServiceResponse<Usuario>>Execute(int id);
    }
}
