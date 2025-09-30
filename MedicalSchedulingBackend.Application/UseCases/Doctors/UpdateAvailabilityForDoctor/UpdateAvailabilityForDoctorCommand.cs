using Cortex.Mediator;
using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.UpdateAvailabilityForDoctor;

public record UpdateAvailabilityForDoctorCommand
(long IdAvailability,
 UpdateAvailabilityDto UpdateAvailabilityDto) : ICommand<Unit>;