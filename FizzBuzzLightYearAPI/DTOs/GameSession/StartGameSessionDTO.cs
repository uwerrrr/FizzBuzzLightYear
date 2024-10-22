namespace FizzBuzzLightYearAPI.DTOs.GameSession;

public class StartGameSessionDTO
{
    public Guid GameId { get; set; }
    public int DurationSeconds { get; set; }
    
    public string PlayerName { get; set; }
}