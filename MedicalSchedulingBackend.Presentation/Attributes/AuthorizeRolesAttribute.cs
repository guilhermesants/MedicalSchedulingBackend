using Microsoft.AspNetCore.Authorization;

namespace MedicalSchedulingBackend.Presentation.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute() : base() { }

    public AuthorizeRolesAttribute(params string[] roles) : base()
    {
        Roles = string.Join(",", roles);
    }
}
