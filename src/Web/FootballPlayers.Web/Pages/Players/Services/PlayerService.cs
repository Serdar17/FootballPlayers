using System.Text;
using System.Text.Json;
using FootballPlayers.Web.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FootballPlayers.Web.Pages.Players.Services;

public class PlayerService : IPlayerService
{
    private readonly HttpClient _client;

    public PlayerService(HttpClient client)
    {
        _client = client;
    }

    public async Task<ICollection<PlayerModel>> GetPlayersAsync()
    {
        var url = $"{Settings.ApiRoot}/api/players";
        var data = await TryConvertFromResponse<IEnumerable<PlayerModel>>(url);
        return data.ToList();
    }

    public async Task<PlayerModel> GetPlayerAsync(string id)
    {
        var url = $"{Settings.ApiRoot}/api/players/{id}";
        var data = await TryConvertFromResponse<PlayerModel>(url);
        return data;
    }

    public async Task<IEnumerable<string>> GetTeamNamesAsync()
    {
        var url = $"{Settings.ApiRoot}/api/players/team-names";
        var data = await TryConvertFromResponse<IEnumerable<string>>(url);
        return data;
    }

    public async Task SendPlayerAsync(PlayerModel model, bool isEdit)
    {
        var url = $"{Settings.ApiRoot}/api/players";
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        //todo: fix
        HttpResponseMessage response;
        
        if (isEdit)
            response = await _client.PutAsync(url, data);
        else
            response = await _client.PostAsync(url, data);
        
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
    }

    public async Task DeletePlayerAsync(string id)
    {
        var url = $"{Settings.ApiRoot}/api/players/{id}";
        var response = await _client.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
            throw new Exception(content);
    }

    private async Task<T> TryConvertFromResponse<T>(string url)
    {
        var response = await _client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        var data = JsonSerializer.Deserialize<T>(content, 
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? Activator.CreateInstance<T>();

        return data;
    }
}