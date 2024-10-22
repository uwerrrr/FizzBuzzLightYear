using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzLightYearAPI.Models;

public class Question
{
    [Key]
    public Guid QuestionId { get; set; } = Guid.NewGuid();

    [ForeignKey("GameSession")]
    public Guid SessionId { get; set; }

    [Required]
    public int Number { get; set; }
    
    // convert all answers to string for evaluation
    public string ExpectedAnswer { get; set; } = string.Empty;
    public string? PlayerAnswer { get; set; }
    
    public bool? IsCorrect { get; set; }
    
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public GameSession GameSession { get; set; }
}