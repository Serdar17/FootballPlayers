namespace Api.Configuration;

public static class SignalRConfiguration
{
    public static IServiceCollection AddAppSignalR(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }

    public static void UseAppSignalHub(this WebApplication app, string path = "playerhub")
    {
        app.MapHub<ReceiveDataHub>(path);
    }
}