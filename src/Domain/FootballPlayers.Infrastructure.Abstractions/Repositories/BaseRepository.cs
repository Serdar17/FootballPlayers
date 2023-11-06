using FootballPlayers.Domain.Entities;

namespace FootballPlayers.Infrastructure.Abstractions.Repositories;

public interface IBaseRepository<TModel, in TKey> where TModel : BaseEntity<TKey>
{
    Task<IEnumerable<TModel>> GetAllAsync();
    Task<TModel?> GetByIdAsync(TKey id);
    Task CreateAsync(TModel model);
    Task UpdateAsync(TModel model);
    Task DeleteAsync(TKey id);
}