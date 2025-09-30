using MedicalSchedulingBackend.Domain.Entities;

namespace MedicalSchedulingBackend.Domain.Interfaces.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<IEnumerable<Doctor>> GetAvailabilitiesAsync(long? doctorId, CancellationToken cancellationToken = default);
}
