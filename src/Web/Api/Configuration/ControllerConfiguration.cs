namespace Api.Configuration;

public static class ControllerConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAppController(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddValidator();
        

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication UseAppController(this WebApplication app)
    {
        app.UseStaticFiles();

        app.MapControllers();

        return app;
    }
}