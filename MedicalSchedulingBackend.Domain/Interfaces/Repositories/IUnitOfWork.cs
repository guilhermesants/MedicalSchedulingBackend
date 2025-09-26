namespace MedicalSchedulingBackend.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAppointmentStatusRepository AppointmentStatusRepository { get; }
    Task CommitAsync(CancellationToken cancellationToken = default);
}
