using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class AppointmentStatusRepository(MedicalSchedulingContext dbContext) : Repository<AppointmentStatus>(dbContext), IAppointmentStatusRepository
{
}

