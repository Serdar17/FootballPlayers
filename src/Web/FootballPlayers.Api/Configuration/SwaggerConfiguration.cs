using Microsoft.OpenApi.Models;

namespace Api.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Football players catalog API",
                Description = "ASP.NET Core 7.0 Web API"
            });
        });
        
        return services;
    }
    
    public static void UseAppSwagger(this WebApplication app)
    {
        app.UseSwagger();
        
        app.UseSwaggerUI();
    }
}