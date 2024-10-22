namespace FizzBuzzLightYearAPI.DTOs;

public class RuleUpdateDTO
{
    public Guid? RuleId { get; set; }
    public int? DivisibleBy { get; set; }
    public string? ReplaceWith { get; set; } = string.Empty;
}