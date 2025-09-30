using MedicalSchedulingBackend.Domain.Entities;

namespace MedicalSchedulingBackend.Domain.Interfaces.Repositories;

public interface IApppointmentRepository : IRepository<Appointment>
{
    Task<Appointment?> GetByIdAvailabilityAsync(long idAvailability, CancellationToken cancellationToken = default);
    Task<IEnumerable<Appointment>?> GetAllbyIdPatientAsync(long idPatient, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
