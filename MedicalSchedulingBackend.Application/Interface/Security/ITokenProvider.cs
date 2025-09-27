using MedicalSchedulingBackend.Domain.Entities;

namespace MedicalSchedulingBackend.Application.Interface.Security;

public interface ITokenProvider
{
    string GenerateToken(User user);
}
