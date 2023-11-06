using FootballPlayers.Domain.Entities;
using FootballPlayers.Players.Models;

namespace FootballPlayers.Players;

public interface IPlayerService
{
    Task<IEnumerable<Player>> GetPlayersAsync();
    Task CreatePlayer(CreatePlayerModel model);
    Task UpdatePlayer(UpdatePlayerModel model);
    Task DeletePlayer(Guid id);
}