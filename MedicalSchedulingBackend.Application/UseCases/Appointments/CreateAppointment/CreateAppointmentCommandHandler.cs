using Cortex.Mediator;
using Cortex.Mediator.Commands;
using MedicalSchedulingBackend.Application.Common.Exceptions;
using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Enum;

namespace MedicalSchedulingBackend.Application.UseCases.Appointments.CreateAppointment;

public sealed class CreateAppointmentCommandHandler(IUnitOfWork uow) : ICommandHandler<CreateAppointmentCommand, Unit>
{
    private readonly IUnitOfWork _uow = uow;

    public async Task<Unit> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var patient = await _uow.PatientRepository.FirstOrDefaultAsync(x => x.UserId == command.UserId, cancellationToken)
                    ?? throw new NotFoundException("Usuário não encontrado");

        var availability = await _uow.AvailabilityRepository.GetByIdAsync(command.AvailabilityId, cancellationToken)
                            ?? throw new NotFoundException("Disponibilidade não encontrada ou não mais disponível");

        if (availability.Appointments.Any())
        {
            var statusId = availability.Appointments.First().StatusId;

            if (statusId is (int)StatusAppointmentsType.Active
                || statusId is (int)StatusAppointmentsType.Completed)
            {
                throw new BadRequestException("Não é possivel criar agenda com essa disponibilidade");
            }
        }

        var appointment = new Appointment
        {
            PatientId = patient.Id,
            AvailabilityId = command.AvailabilityId,
            Time = command.Time,
            StatusId = (int)StatusAppointmentsType.Active
        };

        availability.Available = false;
        await _uow.AppointmentRepository.AddAsync(appointment, cancellationToken);
        await _uow.CommitAsync(cancellationToken);
        return Unit.Value;
    }
}
