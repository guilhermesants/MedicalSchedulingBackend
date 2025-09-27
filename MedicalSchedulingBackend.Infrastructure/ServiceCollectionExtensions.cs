using MedicalSchedulingBackend.Application.Interface.Security;
using MedicalSchedulingBackend.Domain.Interfaces.Repositories;
using MedicalSchedulingBackend.Infrastructure.Concretes.Repositories;
using MedicalSchedulingBackend.Infrastructure.Context;
using MedicalSchedulingBackend.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalSchedulingBackend.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext(configuration)
                       .AddDbHealthCheck()
                       .AddRepositories()
                       .AddSecurity();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("PostgresConn")
                         ?? throw new InvalidOperationException("String de conexão 'PostgresConn' ausente");

        services.AddDbContext<MedicalSchedulingContext>(opt =>
        {
            opt.UseNpgsql(connString);
        });

        return services;
    }

    private static IServiceCollection AddDbHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks()
                .AddDbContextCheck<MedicalSchedulingContext>("PostgresConn");

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services.AddSingleton<ITokenProvider, TokenProvider>();
        return services;
    }
}
