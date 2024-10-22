namespace FizzBuzzLightYearAPI.DTOs;

public class RuleDTO
{
    public Guid RuleId { get; set; }
    public int DivisibleBy { get; set; }
    public string ReplaceWith { get; set; } = string.Empty;
}