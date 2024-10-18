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
    public int Number { get; set; } // Random number generated

    [Required]
    public string PlayerAnswer { get; set; } = string.Empty; // Player's answer

    [Required]
    public bool IsCorrect { get; set; } // True if the player's answer is correct

    // Navigation property
    public GameSession GameSession { get; set; }
}