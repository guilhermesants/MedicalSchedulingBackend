using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Enum;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.CreateDoctor;

public sealed class CreateDoctorCommandHandler(IUnitOfWork uow) : ICommandHandler<CreateDoctorCommand, long>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<long> Handle(CreateDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctor = await _uow.DoctorRepository.FirstOrDefaultAsync(x => x.Email == command.Email, cancellationToken);

        if (doctor is not null)
            throw new BadRequestException("Já existe usuário cadastrado com este email");

        doctor = new Doctor
        {
            Name = command.Name,
            LicenseNumber = command.LicenseNumber,
            Specialty = command.Specialty,
            Phone = command.Phone,
            Email = command.Email,
            User = new User
            {
                UserName = command.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Email),
                RoleId = (int)RolesType.Doctor
            }
        };

        await _uow.DoctorRepository.AddAsync(doctor, cancellationToken);
        await _uow.CommitAsync(cancellationToken);
        return doctor.Id;
    }
}
