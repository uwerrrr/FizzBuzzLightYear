using FizzBuzzLightYearAPI.DTOs.Question;

namespace FizzBuzzLightYearAPI.DTOs.GameSession;

public class GameSessionResponseDTO
{
    public Guid SessionId { get; set; }
    public List<RuleDTO> Rules { get; set; } = new();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsActive { get; set; }
    public QuestionDTO CurrentQuestion { get; set; } = null!;
}