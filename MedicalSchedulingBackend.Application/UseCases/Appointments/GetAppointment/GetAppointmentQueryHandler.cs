using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Appointments.GetAppointment;

public sealed class GetAppointmentQueryHandler(IUnitOfWork uow) : IQueryHandler<GetAppointmentQuery, IEnumerable<AppointmentDto>>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<IEnumerable<AppointmentDto>> Handle(GetAppointmentQuery query, CancellationToken cancellationToken)
    {
        var patient = await _uow.PatientRepository.FirstOrDefaultAsync(x => x.UserId == query.UserId, cancellationToken)
                    ?? throw new BadRequestException("Pacitente não encontrado");

        var appointments = await _uow.AppointmentRepository.GetAllbyIdPatientAsync(patient.Id, query.PageNumber, query.PageSize, cancellationToken)
                    ?? throw new NotFoundException("Agendamentos não encontrados");

        return appointments.Select(x => new AppointmentDto
        {
            DoctorName = x.Availability.Doctor.Name,
            Specialty = x.Availability.Doctor.Specialty,
            StartTime = x.Availability.StartTime,
            EndTime = x.Availability.EndTime,
            Status = x.AppointmentStatus.Name
        }).ToList();
    }
}
