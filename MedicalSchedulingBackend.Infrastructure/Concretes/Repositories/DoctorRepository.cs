using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class DoctorRepository(MedicalSchedulingContext dbContext) : Repository<Doctor>(dbContext), IDoctorRepository
{
}
