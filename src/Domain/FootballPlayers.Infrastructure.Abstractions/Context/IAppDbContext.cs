using FootballPlayers.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.Infrastructure.Abstractions.Context;

public interface IAppDbContext
{
    DbSet<Player> Players { get; set; }
    DbSet<Team> Teams { get; set; }
    Task SaveAsync(CancellationToken cancellationToken = default);
}