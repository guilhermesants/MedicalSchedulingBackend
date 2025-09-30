using MedicalSchedulingBackend.Domain.Entities;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;

internal class UserRepository(MedicalSchedulingContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await DbContext.Users
                          .Include(x => x.Role)
                          .Include(x => x.Patients)
                          .Include(x => x.Doctors)
                          .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<User?> GetByUserAsync(string userName, CancellationToken cancellationToken = default)
        => await DbContext.Users
                          .Include(x => x.Role)
                          .FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);
}
