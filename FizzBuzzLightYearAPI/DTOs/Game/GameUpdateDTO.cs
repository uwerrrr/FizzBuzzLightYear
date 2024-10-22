using Microsoft.Build.Framework;

namespace FizzBuzzLightYearAPI.DTOs;

public class GameUpdateDTO
{
    public Guid GameId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Author { get; set; } = string.Empty;
    public List<RuleUpdateDTO>? Rules { get; set; } = new List<RuleUpdateDTO>();
}