using AutoMapper;
using FootballPlayers.Domain.Entities;
using FootballPlayers.Domain.Enums;

namespace FootballPlayers.Players.Models;

public class CreatePlayerModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Country Country { get; set; }
    public string TeamName { get; set; }
}

public class CreatePlayerModelProfile : Profile
{
    public CreatePlayerModelProfile()
    {
        CreateMap<CreatePlayerModel, Player>();
    }
}