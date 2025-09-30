using Cortex.Mediator;
using Cortex.Mediator.Commands;

namespace MedicalSchedulingBackend.Application.UseCases.Appointments.CreateAppointment;

public sealed record CreateAppointmentCommand
(long AvailabilityId,
 TimeOnly Time,
 long? UserId = null) : ICommand<Unit>;

