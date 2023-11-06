using FootballPlayers.Infrastructure.Context;
using FootballPlayers.Players;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddAppContext()
            .AddPlayerService();

        return services;
    }
}