using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class AppointmentRepository(MedicalSchedulingContext dbContext) : Repository<Appointment>(dbContext), IApppointmentRepository
{
}
