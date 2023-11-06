using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Players;

public static class DependencyInjection
{
    public static IServiceCollection AddPlayerService(this IServiceCollection services)
    {
        services.AddScoped<IPlayerService, PlayerService>();
        
        return services;
    }
}