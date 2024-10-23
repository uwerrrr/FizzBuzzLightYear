namespace FizzBuzzLightYearAPI.DTOs.GameSession;

public class GameSessionStatsDTO
{
    public int CorrectAnswerNum { get; set; }
    public int IncorrectAnswerNum { get; set; }
    public double AccuracyPercentage => 
        (CorrectAnswerNum + IncorrectAnswerNum) == 0 ? 0 : 
            (double)CorrectAnswerNum / (CorrectAnswerNum + IncorrectAnswerNum) * 100;
}