using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;


namespace MT.Innovation.WebApiAdmin.Framework
{
    public class RolePolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public RolePolicyProvider(IOptions<AuthorizationOptions> options) : base(options) { }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
                var policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new RoleRequirement(policyName))
                .Build();

                return policy;
        }
    }
}