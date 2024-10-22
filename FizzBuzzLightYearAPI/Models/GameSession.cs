using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzLightYearAPI.Models;

public class GameSession
{
    [Key]
    public Guid SessionId { get; set; } = Guid.NewGuid();

    [ForeignKey("Game")]
    public Guid GameId { get; set; }

    [Required]
    public string Player { get; set; } = string.Empty;
    
    [Required]
    public int DurationSeconds { get; set; }
    
    [Required]
    public DateTime StartTime { get; set; } = DateTime.UtcNow;

    public DateTime EndTime { get; set; }

    public bool IsActive { get; set; }
    public int CorrectAnswerNum { get; set; }
    public int IncorrectAnswerNum { get; set; }
    public int Score { get; set; }

    // Navigation property
    public Game Game { get; set; }

    // Relationship to Questions
    public List<Question> Questions { get; set; } = new List<Question>();
}