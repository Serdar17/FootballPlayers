using FootballPlayers.Infrastructure.Abstractions.Repositories;

namespace FootballPlayers.Infrastructure.Abstractions.Context;

public interface IUnitOfWork
{
    IPlayerRepository PlayerRepository { get; }
    ITeamRepository TeamRepository { get; }
    Task SaveChangesAsync();
}