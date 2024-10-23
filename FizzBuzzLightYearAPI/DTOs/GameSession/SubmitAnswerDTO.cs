namespace FizzBuzzLightYearAPI.DTOs.GameSession;

public class SubmitAnswerDTO
{
    public Guid SessionId { get; set; }
    public Guid QuestionId { get; set; }
    // public int Number { get; set; }
    public string PlayerAnswer { get; set; } = string.Empty;
}