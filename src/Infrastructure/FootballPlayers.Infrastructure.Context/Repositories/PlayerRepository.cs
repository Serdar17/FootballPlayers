using FootballPlayers.Common.Exeptions;
using FootballPlayers.Domain.Entities;
using FootballPlayers.Infrastructure.Abstractions.Context;
using FootballPlayers.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.Infrastructure.Context.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IAppDbContext _context;

    public PlayerRepository(IAppDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Player>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Player?> GetByIdAsync(Guid id)
    {
        return await _context.Players
                .Include(x => x.Team)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public Task CreateAsync(Player model)
    {
        throw new NotImplementedException();
    }

    // TODO: return updated player
    public Task UpdateAsync(Player model)
    {
        var entity = _context.Players.Update(model);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        ProcessException.ThrowIf(() => entity is null, $"Player with id={id} was not found");
        _context.Players.Remove(entity);
    }
}