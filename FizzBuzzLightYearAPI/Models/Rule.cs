using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FizzBuzzLightYearAPI.Models;

public class Rule
{
    [Key]
    public Guid RuleId { get; set; } = Guid.NewGuid();

    [ForeignKey("Game")]
    public Guid GameId { get; set; }

    [Required]
    public int DivisibleBy { get; set; } // e.g., 7

    [Required]
    public string ReplaceWith { get; set; } = string.Empty; // e.g., "Foo"

    // Navigation property
    public Game Game { get; set; }
    
}