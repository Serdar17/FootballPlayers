using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Common.Helpers;

public static class AutoMappersRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var ass = assemblies.Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("football")).ToList();
        
        services.AddAutoMapper(ass);
    }
}