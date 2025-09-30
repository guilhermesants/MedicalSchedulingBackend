using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Appointments.GetAppointment;

public sealed record GetAppointmentQuery(long? UserId = null, int PageNumber = 1, int PageSize = 10) : IQuery<IEnumerable<AppointmentDto>>;
