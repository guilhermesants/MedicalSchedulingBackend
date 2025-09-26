using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.AppointmentStatus.GetAppointmentStatus;

public record AppointmentStatusQuery() : IQuery<IEnumerable<AppointmentStatusDto>>;
