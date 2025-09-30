using FluentValidation;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.UpdateAvailabilityForDoctor;

public sealed class UpdateAvailabilityForDoctorCommandValidator : AbstractValidator<UpdateAvailabilityForDoctorCommand>
{
    public UpdateAvailabilityForDoctorCommandValidator()
    {
        RuleFor(x => x.UpdateAvailabilityDto.Date)
           .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
           .WithMessage("A data da disponibilidade não pode ser no passado");

        RuleFor(x => x.UpdateAvailabilityDto.StartTime)
            .NotEmpty()
            .WithMessage("O horário de início é obrigatório");

        RuleFor(x => x.UpdateAvailabilityDto.EndTime)
            .NotEmpty()
            .WithMessage("O horário de término é obrigatório");

        RuleFor(x => x)
            .Must(x => x.UpdateAvailabilityDto.StartTime < x.UpdateAvailabilityDto.EndTime)
            .WithMessage("O horário de início deve ser menor que o horário de término");
    }
}

