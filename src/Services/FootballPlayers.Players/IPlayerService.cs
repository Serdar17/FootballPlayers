using FootballPlayers.Players.Models;

namespace FootballPlayers.Players;

public interface IPlayerService
{
    Task<IEnumerable<PlayerModel>> GetPlayersAsync();
    Task<PlayerModel> GetPlayerByIdAsync(Guid id);
    Task<IEnumerable<string>> GetTeamNamesAsync();
    Task CreatePlayer(CreatePlayerModel model);
    Task UpdatePlayer(UpdatePlayerModel model);
    Task DeletePlayer(Guid id);
}