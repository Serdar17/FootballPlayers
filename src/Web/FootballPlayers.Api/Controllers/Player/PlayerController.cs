using Api.Controllers.Player.Models;
using AutoMapper;
using FootballPlayers.Common.Responses;
using FootballPlayers.Players;
using FootballPlayers.Players.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Player;

[Route("api/players")]
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public PlayerController(IPlayerService playerService, IMapper mapper)
    {
        _playerService = playerService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PlayerModel>), 200)]
    public async Task<IActionResult> GetPlayers()
    {
        var models = await _playerService.GetPlayersAsync();

        return Ok(models.Select(x => _mapper.Map<PlayerModel>(x)));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerRequest request)
    {
        var model = _mapper.Map<CreatePlayerModel>(request);
        await _playerService.CreatePlayer(model);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdatePlayer([FromBody] UpdatePlayerRequest request)
    {
        var model = _mapper.Map<UpdatePlayerModel>(request);
        await _playerService.UpdatePlayer(model);
        return Ok();
    }
    
    [ProducesResponseType(typeof(NoContentResult), 204)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePlayer([FromRoute] Guid id)
    {
        await _playerService.DeletePlayer(id);
        return NoContent();
    }
}