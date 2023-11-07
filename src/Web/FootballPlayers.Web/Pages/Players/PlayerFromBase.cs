using FluentValidation;
using FootballPlayers.Web.Models;
using FootballPlayers.Web.Pages.Players.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Severity = MudBlazor.Severity;

namespace FootballPlayers.Web.Pages.Players;

public class PlayerFromBase : ComponentBase
{
    [Inject] protected IPlayerService PlayerService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    protected MudForm _form;
    protected PlayerFormValidator _playerFormValidator = new();
    [CascadingParameter(Name = "Model")] protected PlayerModel? Model { get; set; }
    [CascadingParameter(Name = "Title")] protected string Title { get; set; }
    [CascadingParameter(Name = "EditMode")] protected bool IsEdit { get; set; }
    protected bool IsProcessing { get; set; }
    private IEnumerable<string> _teamNames = new List<string>();
    
    protected override async Task OnInitializedAsync()
    {
        try
        {
            Console.WriteLine("Get names from api");
            await ReloadData();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        };
    }

    private async Task ReloadData()
    {
        _teamNames = await PlayerService.GetTeamNamesAsync();
    }
    
    protected async Task<IEnumerable<string>> Search(string value)
    {
        if (string.IsNullOrEmpty(value))
            return _teamNames;
        return _teamNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }

    protected async Task SendDataAsync()
    {
        await _form.Validate();
        
        if (_form.IsValid)
        {
            IsProcessing = true;
            await PlayerService.SendPlayerAsync(Model, IsEdit);
            Snackbar.Add("Submitted!", Severity.Success);
            IsProcessing = false;
        }
        else
        {
            Snackbar.Add("Validation Error", Severity.Error);
        }

        await ReloadData();
    }
}

public class PlayerFormValidator : AbstractValidator<PlayerModel>
{
    public PlayerFormValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 100);

        RuleFor(x => x.Surname)
            .NotEmpty()
            .Length(1, 100);

        RuleFor(x => x.Gender)
            .NotEmpty();

        RuleFor(x => x.Country)
            .NotEmpty();

        RuleFor(x => x.TeamName)
            .NotEmpty()
            .Length(1, 100);

        RuleFor(x => x.BirthDate)
            .NotEmpty();
    }
    
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<PlayerModel>.CreateWithOptions((PlayerModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}