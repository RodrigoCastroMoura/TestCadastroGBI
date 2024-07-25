using FluentValidation;

namespace GbiTestCadastro.Domain.Validations.Usuarios;

public class UsuarioCreateValidation : AbstractValidator<Entities.Usuario>
{
    public UsuarioCreateValidation()
    {

        RuleFor(usuario => usuario.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(2, 100).WithMessage("O nome deve ter entre 2 e 100 caracteres.");

        RuleFor(usuario => usuario.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email deve ser válido.");

        RuleFor(usuario => usuario.Username)
            .NotEmpty().WithMessage("O Username de usuário é obrigatório.")
            .Length(5, 50).WithMessage("O Username de usuário deve ter entre 5 e 50 caracteres.");

        RuleFor(usuario => usuario.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .Length(6, 100).WithMessage("A senha deve ter entre 6 e 100 caracteres.");

    }
}
