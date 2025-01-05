using Microsoft.AspNetCore.Authorization;

namespace MT.Innovation.WebApiAdmin.Framework
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Role { get; private set; }

        public RoleRequirement(string role)
        {
            Role = role;
        }
    }
}