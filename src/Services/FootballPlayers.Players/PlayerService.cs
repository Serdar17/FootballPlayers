using AutoMapper;
using FootballPlayers.Common.Exeptions;
using FootballPlayers.Common.Extensions;
using FootballPlayers.Domain.Entities;
using FootballPlayers.Infrastructure.Abstractions.Context;
using FootballPlayers.Players.Models;

namespace FootballPlayers.Players;

public class PlayerService : IPlayerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PlayerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlayerModel>> GetPlayersAsync()
    {
        var players = await _unitOfWork.PlayerRepository.GetAllAsync();
        return players.Select(x => _mapper.Map<PlayerModel>(x));
    }

    public async Task<PlayerModel> GetPlayerByIdAsync(Guid id)
    {
        var player = await _unitOfWork.PlayerRepository.GetByIdAsync(id);
        return _mapper.Map<PlayerModel>(player);
    }

    public async Task<IEnumerable<string>> GetTeamNamesAsync()
    {
        return await _unitOfWork.TeamRepository.GetNamesAsync();
    }

    public async Task CreatePlayer(CreatePlayerModel model)
    {
        var team = await _unitOfWork.TeamRepository.GetByNameAsync(model.TeamName.ToTitleCase()) 
                   ?? new Team { Name = model.TeamName.ToTitleCase() };

        var player = _mapper.Map<Player>(model);
        team.Players.Add(player);
        await _unitOfWork.TeamRepository.UpdateAsync(team);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdatePlayer(UpdatePlayerModel model)
    {
        var player = await _unitOfWork.PlayerRepository.GetByIdAsync(model.Id);
        ProcessException.ThrowIf(() => player is null, $"Player with id={model.Id} was not found");

        if (player.Team.Name.Equals(model.TeamName.ToTitleCase()))
        {
            _mapper.Map(model, player);
            await _unitOfWork.PlayerRepository.UpdateAsync(player);

            await _unitOfWork.SaveChangesAsync();
            return;
        }
        
        player.Team.Players.Remove(player);
            
        var team = await _unitOfWork.TeamRepository.GetByNameAsync(model.TeamName.ToTitleCase()) ??
                   new Team { Name = model.TeamName.ToTitleCase() };

        _mapper.Map(model, player);
        team.Players.Add(player);
        await _unitOfWork.TeamRepository.UpdateAsync(team);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeletePlayer(Guid id)
    {
        await _unitOfWork.PlayerRepository.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}