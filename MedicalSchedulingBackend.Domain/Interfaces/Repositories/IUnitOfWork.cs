namespace MedicalSchedulingBackend.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAppointmentStatusRepository AppointmentStatusRepository { get; }
    IUserRepository UserRepository { get; }
    IDoctorRepository DoctorRepository { get; }
    IAvailabilityRepository AvailabilityRepository { get; }
    Task CommitAsync(CancellationToken cancellationToken = default);
}
