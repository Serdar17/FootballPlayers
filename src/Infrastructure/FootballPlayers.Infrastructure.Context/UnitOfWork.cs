using FootballPlayers.Infrastructure.Abstractions.Context;
using FootballPlayers.Infrastructure.Abstractions.Repositories;

namespace FootballPlayers.Infrastructure.Context;

public class UnitOfWork : IUnitOfWork
{
    private readonly Lazy<IPlayerRepository> _playerRepository;
    private readonly Lazy<ITeamRepository> _teamRepository;
    private readonly IAppDbContext _dbContext;

    public UnitOfWork(Lazy<IPlayerRepository> playerRepository, 
        Lazy<ITeamRepository> teamRepository, 
        IAppDbContext dbContext)
    {
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
        _dbContext = dbContext;
    }
    
    public IPlayerRepository PlayerRepository => _playerRepository.Value;
    
    public ITeamRepository TeamRepository => _teamRepository.Value;
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveAsync();
    }
}