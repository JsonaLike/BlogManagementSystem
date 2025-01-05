using Application.Core.Services;
using ERP.Domain.Core.Services;
using ERP.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using MT.Innovation.Shared.Infrastructure;
using MT.Innovation.WebApiAdmin.Framework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, RolePolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, RoleAuthorizationHandler>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<AuthorService>();
_ = builder.Services.AddScoped<ApplicationInfo>(provider =>
{
    var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
    var routeData = httpContext?.GetRouteData().Values;
    var currentAction = routeData?.GetValueOrDefault("action") as string;
    var actionPath = routeData?.GetValueOrDefault("controller") is string currentController
        ? $"{currentController}/{currentAction}"
        : httpContext?.Request?.Path.ToString();


    var appContext = new ApplicationInfo
    {
        CurrentUserName = httpContext?.User?.Identity?.Name ?? "AnonymousUser", //for test
        CorrelationId = httpContext?.TraceIdentifier,
        CurrentActionPath = actionPath,
        CurrentDateTime = DateTime.Now,
        ApplicationRootPath = builder.Environment.ContentRootPath,
    };

    return appContext;
});
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var applicationInfo = scope.ServiceProvider.GetRequiredService<ApplicationInfo>();
    var initializer = new DataInitializer(scope.ServiceProvider, applicationInfo);
    initializer.Initialize();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
