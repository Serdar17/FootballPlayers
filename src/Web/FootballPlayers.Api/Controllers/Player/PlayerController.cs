using Api.Controllers.Player.Models;
using AutoMapper;
using FootballPlayers.Common.Responses;
using FootballPlayers.Players;
using FootballPlayers.Players.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers.Player;

[Route("api/players")]
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;
    private readonly IHubContext<ReceiveDataHub> _hub;

    public PlayerController(IPlayerService playerService, IMapper mapper, IHubContext<ReceiveDataHub> hub)
    {
        _playerService = playerService;
        _mapper = mapper;
        _hub = hub;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlayerModel>), 200)]
    public async Task<IActionResult> GetPlayers()
    {
        var models = await _playerService.GetPlayersAsync();
        return Ok(models);
    }

    [HttpGet("team-names")]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    public async Task<IActionResult> GetTeamNames()
    {
        var names = await _playerService.GetTeamNamesAsync();
        return Ok(names);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PlayerModel), 200)]
    public async Task<IActionResult> GetPlayer([FromRoute] Guid id)
    {
        var model = await _playerService.GetPlayerByIdAsync(id);
        return Ok(model);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NoContent), 204)]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequest request)
    {
        var model = _mapper.Map<CreatePlayerModel>(request);
        await _playerService.CreatePlayer(model);
        await SendDataAsync();
        return NoContent();
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(NoContent), 204)]
    public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerRequest request)
    {
        var model = _mapper.Map<UpdatePlayerModel>(request);
        await _playerService.UpdatePlayer(model);
        await SendDataAsync();
        return NoContent();
    }
    
    [ProducesResponseType(typeof(NoContentResult), 204)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
    {
        await _playerService.DeletePlayer(id);
        await SendDataAsync();
        return NoContent();
    }

    private async Task SendDataAsync()
    {
        var models = await _playerService.GetPlayersAsync();
        await _hub.Clients.All.SendAsync("ReceiveData", models.Select(x => _mapper.Map<PlayerModel>(x)));
    }
}