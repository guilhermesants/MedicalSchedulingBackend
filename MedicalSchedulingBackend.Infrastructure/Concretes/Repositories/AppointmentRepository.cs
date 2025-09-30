using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class AppointmentRepository(MedicalSchedulingContext dbContext) : Repository<Appointment>(dbContext), IApppointmentRepository
{
    public async Task<Appointment?> GetByIdAvailabilityAsync(long idAvailability, CancellationToken cancellationToken = default)
    {
        var appointment = await DbContext.Appointments
                                        .Include(x => x.Availability)
                                        .ThenInclude(x => x.Doctor)
                                        .FirstOrDefaultAsync(x => x.AvailabilityId == idAvailability, cancellationToken);

        return appointment;
    }
}
