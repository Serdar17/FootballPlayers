using FootballPlayers.Domain.Entities;
using FootballPlayers.Domain.Enums;
using FootballPlayers.Infrastructure.Abstractions.Context;
using Microsoft.Extensions.DependencyInjection;

namespace FootballPlayers.Infrastructure.Context.Setup;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    private static IAppDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IAppDbContext>();

    public static void Execute(IServiceProvider serviceProvider, bool addDemoData)
    {
        using var scope = ServiceScope(serviceProvider);
        ArgumentNullException.ThrowIfNull(scope);

        if (addDemoData)
        {
            Task.Run(async () =>
            {
                await ConfigureDemoData(serviceProvider);
            });
        }
    }

    private static async Task ConfigureDemoData(IServiceProvider serviceProvider)
    {
        await AddPlayersAsync(serviceProvider);
    }

    private static async Task AddPlayersAsync(IServiceProvider serviceProvider)
    {
        var context = DbContext(serviceProvider);
    
        if (context.Players.Any() || context.Teams.Any())
            return;

        var messi = new Player
        {
            Name = "Lionel",
            Surname = "Messi",
            Gender = Gender.Male,
            Country = Country.USA,
            BirthDate = new DateTime(1984, 06, 24),
        };

        var mostovoy = new Player
        {
            Name = "Andrey",
            Surname = "Mostovoy",
            Gender = Gender.Male,
            Country = Country.Russia,
            BirthDate = new DateTime(1997, 11, 05),
        };

        var acherby = new Player
        {
            Name = "Acherby",
            Surname = "Franchesco",
            Gender = Gender.Male,
            Country = Country.Russia,
            BirthDate = new DateTime(1988, 02, 10),
        };

        context.Teams.Add(new Team
        {
            Name = "Inter Miami",
            Players = new List<Player>{ messi }
        });
        
        context.Teams.Add(new Team
        {
            Name = "Milan",
            Players = new List<Player>{ acherby }
        });
        
        context.Teams.Add(new Team
        {
            Name = "Zenit",
            Players = new List<Player>{ mostovoy }
        });
    
        await context.SaveAsync();
    }
}