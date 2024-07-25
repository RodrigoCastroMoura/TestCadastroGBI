using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace GbiTestCadastro.Dto.Usuarios;

public class UsuarioDto
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string Senha { get; set; }

    public DateTime Data { get; set; }
}


