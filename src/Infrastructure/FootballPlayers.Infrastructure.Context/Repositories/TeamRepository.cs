using FootballPlayers.Common.Exeptions;
using FootballPlayers.Domain.Entities;
using FootballPlayers.Infrastructure.Abstractions.Context;
using FootballPlayers.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FootballPlayers.Infrastructure.Context.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly IAppDbContext _context;

    public TeamRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Team?> GetByNameAsync(string teamName)
    {
        var team = await _context
            .Teams
            .FirstOrDefaultAsync(x => x.Name.Equals(teamName));

        return team;
    }

    public async Task<IEnumerable<string>> GetNamesAsync()
    {
        return _context.Teams.Select(x => x.Name);
    }

    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return _context.Teams
            .Include(x => x.Players)
            .AsEnumerable();
    }

    public Task<Team?> GetByIdAsync(Guid id)
    {
        return _context.Teams
            .Include(x => x.Players)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task CreateAsync(Team model)
    {
        await _context.Teams.AddAsync(model);
    }

    public async Task UpdateAsync(Team model)
    {
        _context.Teams.Update(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Teams
            .Where(x => x.Id.Equals(id))
            .ExecuteDeleteAsync();
    }
}