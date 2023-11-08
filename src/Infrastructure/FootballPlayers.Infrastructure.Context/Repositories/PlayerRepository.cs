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

    public async Task<IEnumerable<Player>> GetAllAsync()
    {
        return await _context.Players
            .Include(x => x.Team)
            .ToListAsync();
    }

    public async Task<Player?> GetByIdAsync(Guid id)
    {
        return await _context.Players
                .Include(x => x.Team)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task CreateAsync(Player model)
    {
        await _context.Players.AddAsync(model);
    }
    
    public async Task UpdateAsync(Player model)
    {
       _context.Players.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Players
            .Where(x => x.Id.Equals(id))
            .ExecuteDeleteAsync();
    }
}