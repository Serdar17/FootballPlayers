using FootballPlayers.Domain.Entities;

namespace FootballPlayers.Infrastructure.Abstractions.Repositories;

public interface ITeamRepository : IBaseRepository<Team, Guid>
{
    Task<Team?> GetByNameAsync(string teamName);
    Task<IEnumerable<string>> GetNamesAsync();
}