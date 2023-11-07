using FootballPlayers.Web.Models;

namespace FootballPlayers.Web.Pages.Players.Services;

public interface IPlayerService
{
    Task<ICollection<PlayerModel>> GetPlayersAsync();
    Task<PlayerModel> GetPlayerAsync(string id);
    Task<IEnumerable<string>> GetTeamNamesAsync();
    Task SendPlayerAsync(PlayerModel model, bool IsEdit = false);
    Task DeletePlayerAsync(string id);
}