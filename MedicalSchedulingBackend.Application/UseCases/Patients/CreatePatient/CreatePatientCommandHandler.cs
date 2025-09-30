using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Enum;

namespace MedicalSchedulingBackend.Application.UseCases.Patients.CreatePatient;

public sealed class CreatePatientCommandHandler(IUnitOfWork uow) : ICommandHandler<CreatePatientCommand, long>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<long> Handle(CreatePatientCommand command, CancellationToken cancellationToken)
    {
        var patiente = await _uow.PatientRepository.FirstOrDefaultAsync(x => x.Email == command.Email, cancellationToken);

        if (patiente is not null)
            throw new BadRequestException("Paciente ja cadastrado");

        var patient = new Patient
        {
            Name = command.Name,
            Email = command.Email,
            Cpf = command.Cpf,
            Phone = command.Phone,
            User = new User
            {
                UserName = command.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Email),
                RoleId = (int)RolesType.Patient,
            }
        };

        await _uow.PatientRepository.AddAsync(patient, cancellationToken);
        await _uow.CommitAsync(cancellationToken);
        return patient.Id;
    }
}
