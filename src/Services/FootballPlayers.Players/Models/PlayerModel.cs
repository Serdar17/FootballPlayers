using AutoMapper;
using FootballPlayers.Domain.Entities;
using FootballPlayers.Domain.Enums;

namespace FootballPlayers.Players.Models;

public class PlayerModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Country Country { get; set; }
    public string TeamName { get; set; }
}

public class PlayerModelProfile : Profile
{
    public PlayerModelProfile()
    {
        CreateMap<Player, PlayerModel>()
            .ForMember(x => x.TeamName, opt => opt.MapFrom(x => x.Team.Name));
    }
}