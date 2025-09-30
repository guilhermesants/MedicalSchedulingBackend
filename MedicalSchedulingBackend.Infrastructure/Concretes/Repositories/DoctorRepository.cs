using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class DoctorRepository(MedicalSchedulingContext dbContext) : Repository<Doctor>(dbContext), IDoctorRepository
{
    public async Task<IEnumerable<Doctor>> GetAvailabilitiesAsync(long? doctorId, CancellationToken cancellationToken = default)
    {
        var doctorsAvailabilites = await DbContext.Doctors
                                                  .Include(x => x.Availabilities)
                                                  .Where(x => !doctorId.HasValue || x.Id == doctorId.Value)
                                                  .ToListAsync(cancellationToken);

        return doctorsAvailabilites;
    }
    
}
