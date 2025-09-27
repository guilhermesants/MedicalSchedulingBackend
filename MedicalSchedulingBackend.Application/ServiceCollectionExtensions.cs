using FluentValidation;
using Cortex.Mediator.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation.AspNetCore;

namespace MedicalSchedulingBackend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();

        services.AddCortexMediator(
            configuration: configuration,
            handlerAssemblyMarkerTypes: new[] { typeof(ServiceCollectionExtensions) },
            configure: opt =>
            {
                opt.AddDefaultBehaviors();
            });

        return services;
    }
}
