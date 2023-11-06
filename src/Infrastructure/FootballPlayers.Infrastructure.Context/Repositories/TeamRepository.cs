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

    public async Task<Team?> GetByName(string teamName)
    {
        var team = await _context
            .Teams
            .FirstOrDefaultAsync(x => x.Name.Equals(teamName));

        return team;
    }
    public Task<IEnumerable<Team>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Team?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Team model)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Team model)
    {
        _context.Teams.Update(model);
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }


}