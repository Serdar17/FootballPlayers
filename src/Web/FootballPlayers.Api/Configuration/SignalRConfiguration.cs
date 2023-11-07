using Microsoft.AspNetCore.ResponseCompression;

namespace Api.Configuration;

public static class SignalRConfiguration
{
    public static IServiceCollection AddAppSignalR(this IServiceCollection services)
    {
        services.AddSignalR();
        // services.AddResponseCompression(opts =>
        // {
        //     opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        //         new[] { "application/octet-stream" });
        // });

        return services;
    }

    public static void UseAppSignalHub(this WebApplication app, string path = "playerhub")
    {
        // app.UseResponseCompression();
        
        app.MapHub<ReceiveDataHub>(path);
    }
}