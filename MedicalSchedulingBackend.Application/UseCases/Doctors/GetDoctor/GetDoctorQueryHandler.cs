using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.GetDoctor;

public sealed class GetDoctorQueryHandler(IUnitOfWork uow) : IQueryHandler<GetDoctorQuery, DoctorDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<DoctorDto> Handle(GetDoctorQuery query, CancellationToken cancellationToken)
    {
        var result = await _uow.DoctorRepository.FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken)
                            ?? throw new NotFoundException("Médico não encontrado");

        return new DoctorDto
        {
            Name = result.Name,
            Specialty = result.Specialty,
            Phone = result.Phone,
            Email = result.Email,
            LicenseNumber = result.LicenseNumber
        };
    }
}
