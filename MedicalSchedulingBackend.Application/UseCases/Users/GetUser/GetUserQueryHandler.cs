using Cortex.Mediator.Queries;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Application.Dtos;
using MedicalSchedulingBackend.Application.Security.Roles;

namespace MedicalSchedulingBackend.Application.UseCases.Users.GetUser;

public sealed class GetUserQueryHandler(IUnitOfWork uow) : IQueryHandler<GetUserQuery, UserDto>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<UserDto> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _uow.UserRepository.GetByIdAsync(query.IdUser, cancellationToken)
                    ?? throw new NotFoundException("Usuário não encontrado");

        var userDto = new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            LastLogin = user.LastLogin,
            Active = user.Active,
            PerfilName = user.Role.Name
        };

        switch (user.Role.Name)
        {
            case Role.Doctor:
                userDto.DoctorDto = user.Doctors
                    .Select(d => new DoctorDto
                    {
                        Name = d.Name,
                        Specialty = d.Specialty,
                        Phone = d.Phone,
                        LicenseNumber = d.LicenseNumber,
                        Email = d.Email
                    }).FirstOrDefault();
                break;
            case Role.Patient:
                userDto.PatientDto = user.Patients
                    .Select(p => new PatientDto
                    {
                        Name = p.Name,
                        Cpf = p.Cpf,
                        Phone = p.Phone,
                        Email = p.Email
                    }).FirstOrDefault();
                break;
        }

        return userDto;
    }
}
