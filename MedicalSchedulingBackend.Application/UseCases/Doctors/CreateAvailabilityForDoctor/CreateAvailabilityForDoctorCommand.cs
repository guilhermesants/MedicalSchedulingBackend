using Cortex.Mediator.Commands;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.CreateAvailabilityForDoctor;

public sealed record CreateAvailabilityForDoctorCommand
(DateOnly Date,
TimeOnly StartTime,
TimeOnly EndTime,
long? IdUserDoctor = null) : ICommand<long>;