using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Common.Helpers;

public static class AutoMappersRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("crm."));

        services.AddAutoMapper(assemblies);
    }
}