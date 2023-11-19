using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Repository;

namespace WebApi.Configurations;

public static class IOC
{
    public static void DependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationAppService, AuthAppService>();
        services.AddScoped<ISectionsAppService, SectionsAppService>();
        services.AddScoped<IIncidentsAppService, IncidentsAppService>();

        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ISectionsRepository, SectionsRepository>();
        services.AddScoped<IIncidentsRepository, IncidentsRepository>();
    }
}