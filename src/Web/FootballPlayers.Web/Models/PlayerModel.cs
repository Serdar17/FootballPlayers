using FootballPlayers.Domain.Enums;

namespace FootballPlayers.Web.Models;

public class PlayerModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime? BirthDate { get; set; } = DateTime.Today;
    public Country Country { get; set; }
    public string TeamName { get; set; }
}

