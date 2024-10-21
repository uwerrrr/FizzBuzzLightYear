using FizzBuzzLightYearAPI.Models;

namespace FizzBuzzLightYearAPI.DTOs;

public class GameWithRulesCreateDto
{
    public Game Game { get; set; }
    public List<Rule> Rules { get; set; }
}