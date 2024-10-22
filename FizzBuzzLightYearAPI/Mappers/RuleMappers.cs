using FizzBuzzLightYearAPI.DTOs;
using FizzBuzzLightYearAPI.Models;

namespace FizzBuzzLightYearAPI.Mappers;

public static class RuleMappers
{
    public static RuleDTO MapRuleToDTO(this Rule rule)
    {
        return new RuleDTO
        {
            RuleId = rule.RuleId,
            DivisibleBy = rule.DivisibleBy,
            ReplaceWith = rule.ReplaceWith
        };
    }
}