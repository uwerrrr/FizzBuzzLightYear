namespace FizzBuzzLightYearAPI.DTOs;

public class GameDTO
{
    public Guid GameId { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; } = string.Empty;
    
    public string Author { get; set; } = string.Empty;
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public List<RuleDTO> Rules { get; set; } = new List<RuleDTO>();
}