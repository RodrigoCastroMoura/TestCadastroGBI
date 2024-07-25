using GbiTestCadastro.Domain.Data;
using GbiTestCadastro.Domain.Entities;
using GbiTestCadastro.Domain.Repositories.Sql;

namespace GbiTestCadastro.Application.Usecases.Usuarios.Delete
{
    public  class UsuarioDeleteUsecases : IUsuarioDeleteUsecases
    {
        private readonly IUsuarioRepository iUsuarioRepository;
        public UsuarioDeleteUsecases(IUsuarioRepository iUsuarioRepository)
        {
            this.iUsuarioRepository = iUsuarioRepository;
        }

        public async Task<ServiceResponse<Usuario>>Execute(int id)
        {
            var response = new ServiceResponse<Usuario>();

            try
            {
                await iUsuarioRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
