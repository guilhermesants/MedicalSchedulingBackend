using MedicalSchedulingBackend.Domain.Entities;

namespace MedicalSchedulingBackend.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUserAsync(string userName, CancellationToken cancellationToken = default);
}
