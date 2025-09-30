using FluentValidation;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.CreateAvailabilityForDoctor;

public class CreateAvailabilityCommandForDoctorCommandValidator : AbstractValidator<CreateAvailabilityForDoctorCommand>
{
    public CreateAvailabilityCommandForDoctorCommandValidator()
    {
        RuleFor(x => x.Date)
           .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
           .WithMessage("A data da disponibilidade não pode ser no passado");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("O horário de início é obrigatório");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("O horário de término é obrigatório");

        RuleFor(x => x)
            .Must(x => x.StartTime < x.EndTime)
            .WithMessage("O horário de início deve ser menor que o horário de término");
    }
}
