using AutoMapper;
using FluentValidation;
using FootballPlayers.Domain.Enums;
using FootballPlayers.Players.Models;

namespace Api.Controllers.Player.Models;

public class UpdatePlayerRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Country Country { get; set; }
    public string TeamName { get; set; }
}

public class UpdatePlayerRequestValidator : AbstractValidator<UpdatePlayerRequest>
{
    public UpdatePlayerRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}

public class UpdatePlayerRequestProfile : Profile
{
    public UpdatePlayerRequestProfile()
    {
        CreateMap<UpdatePlayerRequest, UpdatePlayerModel>();
    }
}