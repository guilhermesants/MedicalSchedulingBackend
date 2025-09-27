using FluentValidation;

namespace MedicalSchedulingBackend.Application.UseCases.Auth.Login;

public sealed class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.User)
            .NotEmpty().WithMessage("O login é obrigatório");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória");
    }
}
