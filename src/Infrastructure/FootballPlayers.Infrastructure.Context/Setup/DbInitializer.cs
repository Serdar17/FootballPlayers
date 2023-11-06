using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Infrastructure.Context.Setup;

public static class DbInitializer
{
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        ArgumentNullException.ThrowIfNull(scope);
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContextFactory.Database.Migrate();
    }
}