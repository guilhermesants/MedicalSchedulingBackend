using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class AppointmentRepository(MedicalSchedulingContext dbContext) : Repository<Appointment>(dbContext), IApppointmentRepository
{
    public async Task<IEnumerable<Appointment>?> GetAllbyIdPatientAsync(long idPatient, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var appointments = await DbContext.Appointments
                                          .Include(x => x.AppointmentStatus)
                                          .Include(x => x.Availability)
                                          .ThenInclude(x => x.Doctor)
                                          .Where(x => x.PatientId == idPatient)
                                          .Skip(pageSize * (pageNumber - 1))
                                          .Take(pageSize)
                                          .ToListAsync(cancellationToken);

        return appointments;
    }

    public async Task<Appointment?> GetByIdAvailabilityAsync(long idAvailability, CancellationToken cancellationToken = default)
    {
        var appointment = await DbContext.Appointments
                                        .Include(x => x.Availability)
                                        .ThenInclude(x => x.Doctor)
                                        .FirstOrDefaultAsync(x => x.AvailabilityId == idAvailability, cancellationToken);

        return appointment;
    }
}
