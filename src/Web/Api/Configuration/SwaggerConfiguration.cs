using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api.Configuration;

public static class SwaggerConfiguration
{
    private static readonly string AppTitle = "Football Players Api";

    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
    
    public static void UseAppSwagger(this WebApplication app)
    {
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "api/{documentname}/api.yaml";
        });
        
        app.UseSwaggerUI(
            options =>
            {
                options.RoutePrefix = "api";
                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(-1);
                options.OAuthAppName(AppTitle);
            }
        );
    }
}