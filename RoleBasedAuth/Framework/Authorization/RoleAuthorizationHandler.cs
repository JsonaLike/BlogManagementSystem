using ERP.Application.Core.Helpers;
using ERP.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MT.Innovation.WebApiAdmin.Framework;

public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
{
    private const string NotAuthenticatedMessage = "User not authenticated!";
    private const string NotAutherizedMessage = "User not autherized to access this resource!";
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;
    private ApplicationDbContext _dbContext;
    public RoleAuthorizationHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ApplicationDbContext dbContext)
    {
        _config = configuration;
        _httpContextAccessor = httpContextAccessor;
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var token = httpContext.Request.Cookies["AuthToken"];
        if(token == null)
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync("Unauthorized");
            context.Fail();
            return;
        }
        var secretKey = _config.GetValue<string>("JWTSecretKey");
        var claims = JWTHelper.ValidateTokenWithLifeTime(token, secretKey);
        if (claims.Any())
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            httpContext.User = user;
        }
        else
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync("Unauthorized");
            context.Fail();
            return;
        }
        var userId = claims.Single(y => y.Type == "nameid").Value;
        var dbUser = _dbContext.Users.Single(x=>x.Id.ToString() == userId);
            var roleId = _dbContext.Roles.Single(y => y.Name == requirement.Role);
        var UserRoles = _dbContext.UserRoles.ToList();
        var hasRole = UserRoles.Any(ur => ur.Role.Name == requirement.Role);
        if (hasRole) {
            context.Succeed(requirement);
            return;
        }
        else
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync("Unauthorized");
            context.Fail();
            return;
        }
    }
}