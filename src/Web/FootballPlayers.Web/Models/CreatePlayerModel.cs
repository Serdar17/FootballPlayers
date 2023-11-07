using FootballPlayers.Domain.Enums;

namespace FootballPlayers.Web.Models;

public class CreatePlayerModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; } = DateTime.Today;
    public Country Country { get; set; }
    public string TeamName { get; set; }
}