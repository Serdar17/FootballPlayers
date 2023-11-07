using FootballPlayers.Web.Components;
using FootballPlayers.Web.Models;
using FootballPlayers.Web.Pages.Players.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;

namespace FootballPlayers.Web.Pages.Players;

public class PlayersBase : ComponentBase, IAsyncDisposable
{
    [Inject] protected IPlayerService PlayerService { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }
    private HubConnection? _hubConnection;
    protected ICollection<PlayerModel> Players = new List<PlayerModel>();

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Console.WriteLine("Get players from api");
            await ConnectToHubAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private async Task ConnectToHubAsync()
    {
        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{Settings.ApiRoot}/playerhub")
            .Build();

        _hubConnection.On<ICollection<PlayerModel>>("ReceiveData", players =>
        {
            Console.WriteLine($"length of players ={players.Count}");
            Players = players;
            InvokeAsync(StateHasChanged);
        });

        await _hubConnection.StartAsync();
    }

    protected async Task OnDeletePlayer(Guid playerId)
    {        
        var parameters = new DialogParameters<Dialog>
        {
            { x => x.ContentText, "Do you really want to delete these records? This process cannot be undone." },
            { x => x.ButtonText, "Delete" },
            { x => x.Color, Color.Error }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Delete", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
            await PlayerService.DeletePlayerAsync(playerId.ToString());
        
        StateHasChanged();
    }

    protected string GetLink(string path, Guid id) => $"/{path}/{id.ToString()}";

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
        }
    }
}