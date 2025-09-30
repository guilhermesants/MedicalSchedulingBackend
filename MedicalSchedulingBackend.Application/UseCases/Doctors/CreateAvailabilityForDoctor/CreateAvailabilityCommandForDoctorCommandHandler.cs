using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Domain.Entities;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.CreateAvailabilityForDoctor;

public sealed class CreateAvailabilityCommandForDoctorCommandHandler(IUnitOfWork uow) : ICommandHandler<CreateAvailabilityForDoctorCommand, long>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<long> Handle(CreateAvailabilityForDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctor = await _uow.DoctorRepository.FirstOrDefaultAsync(x => x.UserId == command.IdUserDoctor, cancellationToken)
                            ?? throw new NotFoundException("Usuário não encontrado");

        var availability = await _uow.AvailabilityRepository
                                     .FirstOrDefaultAsync(x => x.DoctorId == doctor.Id
                                                                && x.Date == command.Date 
                                                                && x.StartTime == command.StartTime
                                                                && x.EndTime ==  command.EndTime,
                                                                cancellationToken);

        if (availability is not null)
            throw new BadRequestException("Já existe uma disponibilidade cadastrada para essa data neste horário");

        availability = new Availability
        {
            DoctorId = doctor.Id,
            Date = command.Date,
            StartTime = command.StartTime,
            EndTime = command.EndTime,
        };

        await _uow.AvailabilityRepository.AddAsync(availability, cancellationToken);
        await _uow.CommitAsync(cancellationToken);
        return doctor.Id;
    }
}
