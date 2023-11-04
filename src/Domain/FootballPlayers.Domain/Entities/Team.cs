namespace FootballPlayers.Domain.Entities;

public class Team : BaseEntity<Guid>
{
    public string Name { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}