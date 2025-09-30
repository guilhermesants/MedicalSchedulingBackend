using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.GetAvailabilities;

public sealed class GetAvailabilitiesQueryHandler(IUnitOfWork uow) : IQueryHandler<GetAvailabilitiesQuery, IEnumerable<DoctorDto>>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<IEnumerable<DoctorDto>> Handle(GetAvailabilitiesQuery query, CancellationToken cancellationToken)
    {
        var result = await _uow.DoctorRepository.GetAvailabilitiesAsync(query.IdDoctor, cancellationToken)
                                    ?? throw new NotFoundException("Disponibilidades não encontradas");

        return result.Select(x => new DoctorDto
        {
            Name = x.Name,
            Specialty = x.Specialty,
            Phone = x.Phone,
            Email = x.Email,
            LicenseNumber = x.LicenseNumber,
            AvailabilitiesDtos = x.Availabilities.Select(a => new AvailabilitiesDto
            {
                Id = a.Id,
                DoctorId = x.Id,
                Date = a.Date,
                StartTime = a.StartTime,
                EndTime = a.EndTime
            })
        });
    }
}
