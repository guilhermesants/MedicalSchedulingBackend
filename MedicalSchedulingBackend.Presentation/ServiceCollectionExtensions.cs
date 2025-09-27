using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace MedicalSchedulingBackend.Presentation;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.EnableAnnotations();
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Medical Scheduling",
                Version = "v1",
                Description = "Api para agendamento medico"
            });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Jwt Authentication",
                Description = "Insira seu token JWT neste campo",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT"
            };

            opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            };

            opt.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }
}
