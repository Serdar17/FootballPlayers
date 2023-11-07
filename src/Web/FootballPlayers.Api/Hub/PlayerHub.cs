using System.Collections;
using AutoMapper;
using FootballPlayers.Players;
using FootballPlayers.Players.Models;
using Microsoft.AspNetCore.SignalR;

namespace Api;

public class ReceiveDataHub : Hub<IReceiveData>
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;
    
    public ReceiveDataHub(IPlayerService playerService, IMapper mapper)
    {
        _playerService = playerService;
        _mapper = mapper;
    }
    
    public override async Task OnConnectedAsync()
    {
        var models = await _playerService.GetPlayersAsync();
        await Clients.Client(Context.ConnectionId)
            .ReceiveData(models.Select(x => _mapper.Map<PlayerModel>(x)).ToList());
        
        await base.OnConnectedAsync();
    }
}

public interface IReceiveData
{
    Task ReceiveData(ICollection<PlayerModel> models);
}