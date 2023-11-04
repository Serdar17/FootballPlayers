using FootballPlayers.Domain.Enums;

namespace FootballPlayers.Domain.Entities;

public class Player : BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Gender Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public Country Country { get; set; }
    
    public Guid TeamId { get; set; }
    public virtual Team Team { get; set; }
}