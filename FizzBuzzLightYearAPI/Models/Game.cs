using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FizzBuzzLightYearAPI.Models;

public class Game
{
    [Key]
    public Guid GameId { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Author { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    // Relationship to Rules
    public ICollection<Rule> Rules { get; set; } = new List<Rule>();
}