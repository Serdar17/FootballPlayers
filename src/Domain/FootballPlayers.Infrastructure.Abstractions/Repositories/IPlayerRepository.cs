using FootballPlayers.Domain.Entities;

namespace FootballPlayers.Infrastructure.Abstractions.Repositories;

public interface IPlayerRepository : IBaseRepository<Player, Guid>
{
    
}