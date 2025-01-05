using Microsoft.AspNetCore.Authorization;

namespace MT.Innovation.WebApiAdmin.Framework;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RoleAuthorizeAttribute : AuthorizeAttribute
{

    public const string DefaultPermission = "Permission.Default.AuthenticatedUser";

    public RoleAuthorizeAttribute(string permission = DefaultPermission) : base(policy:permission)
    {
            
    }
}
