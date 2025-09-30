using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Dtos;

namespace MedicalSchedulingBackend.Application.UseCases.Patients.GetPatient;

public sealed class GetPatientQueryHandler(IUnitOfWork uow) : IQueryHandler<GetPatientQuery, PatientDto>
{
    private readonly IUnitOfWork _uow = uow;
    public async Task<PatientDto> Handle(GetPatientQuery query, CancellationToken cancellationToken)
    {
        var patient = await _uow.PatientRepository.FirstOrDefaultAsync(x => x.Id == query.id, cancellationToken)
                    ?? throw new NotFoundException("Paciente não encontrado");

        return new PatientDto
        {
            Id = patient.Id,
            Name = patient.Name,
            Email = patient.Email,
            Phone = patient.Phone,
            Cpf = patient.Cpf
        };
    }
}
