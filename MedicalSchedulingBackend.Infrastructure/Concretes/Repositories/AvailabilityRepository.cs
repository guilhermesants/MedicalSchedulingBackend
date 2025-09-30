using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class AvailabilityRepository(MedicalSchedulingContext dbContext) : Repository<Availability>(dbContext), IAvailabilityRepository
{
    public async Task<Availability?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var availability = await DbContext.Availabilities
                                          .Include(x => x.Appointments)
                                          .Include(x => x.Doctor)
                                          .FirstOrDefaultAsync(x => x.Id == id && x.Available, cancellationToken);

        return availability;
    }
}
