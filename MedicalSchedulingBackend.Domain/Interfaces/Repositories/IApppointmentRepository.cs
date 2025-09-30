using MedicalSchedulingBackend.Domain.Entities;

namespace MedicalSchedulingBackend.Domain.Interfaces.Repositories;

public interface IApppointmentRepository : IRepository<Appointment>
{
    Task<Appointment?> GetByIdAvailabilityAsync(long idAvailability, CancellationToken cancellationToken = default);
}
