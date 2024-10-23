using FizzBuzzLightYearAPI.DTOs.GameSession;
using FizzBuzzLightYearAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzzLightYearAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameSessionController : ControllerBase
{
    private readonly GameSessionService _gameSessionService;
    private readonly ILogger<GameSessionController> _logger;

    public GameSessionController(
        GameSessionService gameSessionService,
        ILogger<GameSessionController> logger)
    {
        _gameSessionService = gameSessionService;
        _logger = logger;
    }

    /// <summary>
    /// Starts a new game session
    /// </summary>
    /// <param name="request">Game session configuration</param>
    /// <returns>New game session details including first question</returns>
    [HttpPost("start")]
    [ProducesResponseType(typeof(GameSessionResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GameSessionResponseDTO>> StartSession([FromBody] StartGameSessionDTO request)
    {
        try
        {
            var response = await _gameSessionService.StartNewSessionAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error starting new game session");
            return StatusCode(500, new { message = "Error starting game session" });
        }
    }

    /// <summary>
    /// Submits an answer for the current question and gets the next question
    /// </summary>
    /// <param name="answer">Player's answer submission</param>
    /// <returns>Answer validation result and next question if available</returns>
    [HttpPost("answer")]
    [ProducesResponseType(typeof(AnswerResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AnswerResponseDTO>> SubmitAnswer([FromBody] SubmitAnswerDTO answer)
    {
        try
        {
            var response = await _gameSessionService.ProcessAnswerAsync(answer);
            return Ok(response);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            _logger.LogWarning(ex, "Resource not found while processing answer");
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing answer");
            return StatusCode(500, new { message = "Error processing answer" });
        }
    }

    /// <summary>
    /// Gets the statistics for a specific game session
    /// </summary>
    /// <param name="sessionId">ID of the game session</param>
    /// <returns>Session statistics</returns>
    [HttpGet("{sessionId}/stats")]
    [ProducesResponseType(typeof(GameSessionStatsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GameSessionStatsDTO>> GetSessionStats(Guid sessionId)
    {
        try
        {
            var stats = await _gameSessionService.GetSessionStatsAsync(sessionId);
            return Ok(stats);
        }
        catch (Exception ex) when (ex.Message.Contains("not found"))
        {
            _logger.LogWarning(ex, "Session not found while fetching stats");
            return NotFound(new { message = "Session not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving session stats");
            return StatusCode(500, new { message = "Error retrieving session statistics" });
        }
    }
    
    
    
}