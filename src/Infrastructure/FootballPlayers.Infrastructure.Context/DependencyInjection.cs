using FootballPlayers.Infrastructure.Abstractions.Context;
using FootballPlayers.Infrastructure.Abstractions.Repositories;
using FootballPlayers.Infrastructure.Context.Repositories;
using FootballPlayers.Infrastructure.Context.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Infrastructure.Context;

public static class DependencyInjection
{
    private const string MigrationProjectPrefix = "FootballPlayers.Infrastructure.Context";
    
    public static IServiceCollection AddAppContext(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = FootballPlayers.Settings.Settings.Load<DbSettings>(DbSettings.SectionName, configuration);
        services.AddSingleton(settings);

        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseNpgsql(settings.ConnectionString,
                opts => opts
                    .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                    .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                    .MigrationsAssembly($"{MigrationProjectPrefix}")
            );
            opt.UseSnakeCaseNamingConvention();
        });

        services.AddTransient(typeof(Lazy<>));

        services.AddScoped<IAppDbContext, AppDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        
        return services;
    }
}