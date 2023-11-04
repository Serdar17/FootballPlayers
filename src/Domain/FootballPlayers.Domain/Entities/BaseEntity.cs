namespace FootballPlayers.Domain.Entities;

public abstract class BaseEntity<TKey>
{
    public TKey Id { get; set; }
    public DateTime CreateDate { get; init; } = DateTime.Now;
    public DateTime UpdateDate { get; set; } = DateTime.Now;
}