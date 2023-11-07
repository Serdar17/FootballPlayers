using FootballPlayers.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FootballPlayers.Web.Pages.Players;

public class CreatePlayerBase : ComponentBase
{
    protected PlayerModel Model = new();
    protected string Title = "Create player form";
}