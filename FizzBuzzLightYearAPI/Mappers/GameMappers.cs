using FizzBuzzLightYearAPI.DTOs;
using FizzBuzzLightYearAPI.Models;

namespace FizzBuzzLightYearAPI.Mappers;

public static class GameMappers
{
    public static GameDTO MapToGameDTO(this Game gameModel)
    {
        return new GameDTO
        {
            GameId = gameModel.GameId,
            Name = gameModel.Name,
            Author = gameModel.Author,
            CreatedDate = gameModel.CreatedDate,
            Rules = gameModel.Rules.Select(rule => new RuleDTO
            {
                RuleId = rule.RuleId,
                DivisibleBy = rule.DivisibleBy,
                ReplaceWith = rule.ReplaceWith
            }).ToList()
        };
    }
  
}