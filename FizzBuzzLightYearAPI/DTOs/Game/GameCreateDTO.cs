using Microsoft.Build.Framework;

namespace FizzBuzzLightYearAPI.DTOs;

public class GameCreateDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Author { get; set; } = string.Empty;
    
    public List<RuleCreateDTO> Rules { get; set; } = new List<RuleCreateDTO>();
}