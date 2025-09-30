using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MedicalSchedulingBackend.Presentation.Extensions;

internal static class ClaimsPrincipalExtensions
{
    public static long? GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

        return long.TryParse(id, out var userId) ? userId : null;
    }
}
