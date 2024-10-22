using Microsoft.Build.Framework;

namespace FizzBuzzLightYearAPI.DTOs;

public class RuleCreateDTO
{
    [Required]
    public int DivisibleBy { get; set; }

    [Required]
    public string ReplaceWith { get; set; } = string.Empty;
}