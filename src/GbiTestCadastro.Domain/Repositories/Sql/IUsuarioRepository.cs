using GbiTestCadastro.Domain.Entities;

namespace GbiTestCadastro.Domain.Repositories.Sql;

public interface IUsuarioRepository
{
    Task Add(Usuario usuario);

    Task<Usuario> Get(int id);

    Task<IEnumerable<Usuario>> GetAll();

    Task UpdateAsync(Usuario usuario);

    Task DeleteAsync(int id);
}
