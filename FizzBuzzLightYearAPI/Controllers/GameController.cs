using FizzBuzzLightYearAPI.DTOs;
using FizzBuzzLightYearAPI.Models;
using FizzBuzzLightYearAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzLightYearAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _gameService;

    public GameController(GameService gameService)
    {
        _gameService = gameService;
    }
    
    // GET: api/Game
    [HttpGet]
    public async Task<IActionResult> GetAllGamesWithRules()
    {
        var games = await _gameService.GetAllGamesWithRulesAsync();
        return Ok(games);
    }

    // GET: api/Game/{gameId}
    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetAGameWithRulesById(Guid gameId)
    {
        try
        {
            var game = await _gameService.GetAGameWithRulesByIdAsync(gameId);
            return Ok(game);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: api/Game
    [HttpPost]
    public async Task<IActionResult> AddGameWithRules([FromBody] GameWithRulesCreateDto gameWithRulesCreateDto)
    {
        try
        {
            var game = gameWithRulesCreateDto.Game;
            var rules = gameWithRulesCreateDto.Rules;
            
            await _gameService.AddGameWithRulesAsync(game, rules);
            return CreatedAtAction(nameof(GetAGameWithRulesById), new { id = game.GameId }, game);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Game/{gameId}
    [HttpPut("{gameId}")]
    public async Task<IActionResult> UpdateGameWithRules([FromBody] Game gameToUpdate)
    {
        try
        {
            await _gameService.UpdateGameWithRulesAsync(gameToUpdate);
            return Ok($"Game {gameToUpdate.GameId} has been updated");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Game/{gameId}
    [HttpDelete("{gameId}")]
    public async Task<IActionResult> RemoveGameWithRules(Guid gameId)
    {
        try
        {
            await _gameService.RemoveGameWithRulesByIdAsync(gameId);
            return Ok($"Game {gameId} is removed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}
