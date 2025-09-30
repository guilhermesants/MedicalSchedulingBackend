using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.GetAvailabilities;

public record GetAvailabilitiesQuery
(long? IdDoctor = null) : IQuery<IEnumerable<DoctorDto>>;
