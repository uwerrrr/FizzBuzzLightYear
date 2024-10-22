using FizzBuzzLightYearAPI.DTOs;
using FizzBuzzLightYearAPI.Mappers;
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
        
        // Check if the list is null or empty
        if (games == null || !games.Any())
        {
            return NotFound("No games found.");
        }
        
        // Map each game to GameDTO
        var gameDTOs = games.Select(game => game.MapToGameDTO()).ToList();

       
        return Ok(gameDTOs);
    }

    // GET: api/Game/{gameId}
    [HttpGet("{gameId}")]
    public async Task<IActionResult> GetAGameWithRulesById(Guid gameId)
    {
        try
        {
            var game = await _gameService.GetAGameWithRulesByIdAsync(gameId);
            if (game == null)
            {
                return NotFound($"Game with ID {gameId} not found.");
            }
            var gameDTO = game.MapToGameDTO();
            return Ok(gameDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/Game
    [HttpPost]
    public async Task<IActionResult> AddGameWithRules([FromBody] GameCreateDTO newGame)
    {
        try
        {
            var createdGame = await _gameService.AddGameWithRulesAsync(newGame);
            
            // Returns a 201 Created response with the location of the newly created game resource 
            // and a DTO representation of the game, using the GameId to build the URL for retrieval.
            return CreatedAtAction(nameof(GetAGameWithRulesById), new { gameId = createdGame.GameId }, createdGame.MapToGameDTO()); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/Game/{gameId}
    [HttpPut("{gameId}")]
    public async Task<IActionResult> UpdateGameWithRules([FromBody] GameUpdateDTO gameToUpdate)
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
