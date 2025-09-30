using FluentValidation;

namespace MedicalSchedulingBackend.Application.UseCases.Appointments.CreateAppointment;

public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentCommandValidator()
    {
        RuleFor(x => x.AvailabilityId)
            .NotEmpty().WithMessage("O identificador da disponibilidade é obrigatória");

        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("A hora de inicio é obrigatória");
    }
}

