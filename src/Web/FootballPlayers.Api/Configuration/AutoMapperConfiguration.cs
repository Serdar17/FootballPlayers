using FootballPlayers.Common.Helpers;
using FootballPlayers.Players;

namespace Api.Configuration;

/// <summary>
/// AutoMapper configuration
/// </summary>
public static class AutoMapperConfiguration
{
    /// <summary>
    /// Add automappers
    /// </summary>
    /// <param name="services">Services collection</param>
    public static IServiceCollection AddAppAutoMappers(this IServiceCollection services)
    {
        var assembly = typeof(IPlayerService).Assembly;
        AutoMappersRegisterHelper.Register(services);

        return services;
    }
}