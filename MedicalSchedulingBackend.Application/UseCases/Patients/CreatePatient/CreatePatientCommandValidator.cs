using FluentValidation;

namespace MedicalSchedulingBackend.Application.UseCases.Patients.CreatePatient;

public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
{
    public CreatePatientCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O Nome é obrigatório");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O cpf é obrigatório")
            .IsValidCPF().WithMessage("Cpf inválido")
            .MaximumLength(11).WithMessage("O cpf deve conter apenas 11 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O Email é obrigatório")
            .EmailAddress().WithMessage("O email informado não é válido")
            .MaximumLength(100).WithMessage("O email deve ter no máximo 100 caracteres");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório")
            .MaximumLength(20).WithMessage("A teletone deve ter no máximo 20 caracteres");
    }
}
