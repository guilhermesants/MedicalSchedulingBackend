using FluentValidation;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;

public sealed class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O Nome é obrigatório");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("A licensa é obrigatória")
            .MaximumLength(20).WithMessage("A licença deve ter no máximo 20 caracteres");

        RuleFor(x => x.Specialty)
            .NotEmpty().WithMessage("A especialidade é obrigatória")
            .MaximumLength(100).WithMessage("A licença deve ter no máximo 100 caracteres");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("O telefone é obrigatório")
            .MaximumLength(20).WithMessage("A teletone deve ter no máximo 20 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório")
            .EmailAddress().WithMessage("O email informado não é válido")
            .MaximumLength(100).WithMessage("A email deve ter no máximo 100 caracteres");
    }
}
