using Cortex.Mediator.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MedicalSchedulingBackend.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
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
