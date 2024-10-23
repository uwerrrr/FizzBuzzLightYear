using FizzBuzzLightYearAPI.DTOs.Question;

namespace FizzBuzzLightYearAPI.DTOs.GameSession;

public class AnswerResponseDTO
{
    public bool IsCorrect { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
   public QuestionDTO? NextQuestion {get; set;} = new QuestionDTO();
   public string PlayerAnswer { get; set; } = string.Empty;
    public bool GameEnded { get; set; }
    // public GameSessionStatsDTO? FinalStats { get; set; }
}