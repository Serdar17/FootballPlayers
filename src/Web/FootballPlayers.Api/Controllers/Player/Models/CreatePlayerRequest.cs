using AutoMapper;
using FluentValidation;
using FootballPlayers.Domain.Enums;
using FootballPlayers.Players.Models;

namespace Api.Controllers.Player.Models;

public class CreatePlayerRequest
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Country Country { get; set; }
    public string TeamName { get; set; }
}

public class CreatePlayerRequestValidator : AbstractValidator<CreatePlayerRequest>
{
    public CreatePlayerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Birth date is required");

        RuleFor(x => x.TeamName)
            .NotEmpty().WithMessage("Team name is required");
    }
}

public class CreatePlayerRequestProfile : Profile
{
    public CreatePlayerRequestProfile()
    {
        CreateMap<CreatePlayerRequest, CreatePlayerModel>();
    }
}