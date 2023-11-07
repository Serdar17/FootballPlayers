using FootballPlayers.Web.Models;
using FootballPlayers.Web.Pages.Players.Services;
using Microsoft.AspNetCore.Components;

namespace FootballPlayers.Web.Pages.Players;

public class EditPlayerBase : ComponentBase
{
    [Inject] private IPlayerService PlayerService { get; set; }
    [Parameter] public string PlayerId { get; set; }
    protected PlayerModel Model { get; set; }
    protected bool IsEdit => true;
    protected string Title => "Edit player form";

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Console.WriteLine($"Get player with id={PlayerId} from api");
            Model = await PlayerService.GetPlayerAsync(PlayerId);
            Console.WriteLine($"{Model.Id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}