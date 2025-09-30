using Cortex.Mediator;
using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Common.Exceptions;

namespace MedicalSchedulingBackend.Application.UseCases.Doctors.UpdateAvailabilityForDoctor;

public sealed class UpdateAvailabilityForDoctorCommandHandler(IUnitOfWork uow) : ICommandHandler<UpdateAvailabilityForDoctorCommand, Unit>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<Unit> Handle(UpdateAvailabilityForDoctorCommand command, CancellationToken cancellationToken)
    {
        var availability = await _uow.AvailabilityRepository.FirstOrDefaultAsync(x => x.Id == command.IdAvailability, cancellationToken)
                            ?? throw new NotFoundException("Disponibilidade não encontrada");

        availability.Date = command.UpdateAvailabilityDto.Date;
        availability.StartTime = command.UpdateAvailabilityDto.StartTime;
        availability.EndTime = command.UpdateAvailabilityDto.EndTime;
       
        await _uow.CommitAsync(cancellationToken);
        return Unit.Value;
    }
}
