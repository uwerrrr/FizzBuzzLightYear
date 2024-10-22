using FizzBuzzLightYearAPI.DTOs;
using FizzBuzzLightYearAPI.Models;
using FizzBuzzLightYearAPI.Repositories;

namespace FizzBuzzLightYearAPI.Services;

public class GameService
{
    private readonly GameRepository _gameRepo;
    private readonly RuleService _ruleService;

    public GameService(GameRepository gameRepo, RuleService rulesService)
    {
        _gameRepo = gameRepo;
        _ruleService = rulesService;
    }
    
     // get all quesitons with rules
    public async Task<List<Game>> GetAllGamesWithRulesAsync()
    {
        return await _gameRepo.GetAllGamesWithRulesAsync();
    }
    
    // get a game with its rules by id
    public async Task<Game> GetAGameWithRulesByIdAsync(Guid gameId)
    {
        var existingGame = await _gameRepo.GetAGameWithRulesByIdAsync(gameId);
        
        if (existingGame == null)
        {
            throw new Exception($"Game with id {gameId} does not exist");
        }

        return existingGame;
    }
    
    // // get game list with its rules by given id list
    // public async Task<List<Game>> GetGameListWithRulesByIdListAsync(List<Guid> idList)
    // {
    //     return await _gameRepo.GetGameListWithRulesByIdListAsync(idList);
    // }

    // add a game with its rules
    
    public async Task<Game> AddGameWithRulesAsync(GameCreateDTO newGameDTO)
    {
        
        // Validate the number of rules needs to be 3 rules during creation
        if (newGameDTO.Rules.Count != 3)
        {
            throw new ArgumentException("A game must have exactly 3 rules.");
        }
        
        var newGame = new Game
        {
            GameId = Guid.NewGuid(),
            Name = newGameDTO.Name,
            Author = newGameDTO.Author,
            CreatedDate = DateTime.UtcNow,
            Rules = newGameDTO.Rules.Select(r => new Rule
            {
                RuleId = Guid.NewGuid(),
                DivisibleBy = r.DivisibleBy,
                ReplaceWith = r.ReplaceWith
            }).ToList()
        };
        
        
        // Set the GameId for each rule after creating the game
        foreach (var rule in newGame.Rules)
        {
            rule.GameId = newGame.GameId; // Associate the GameId with each Rule
        }
        
        await _gameRepo.AddGameAsync(newGame);
        
        return (newGame);
    }

    
    // update a game with its rules
    public async Task UpdateGameWithRulesAsync(GameUpdateDTO gameToUpdate)
    {
        var existingGame = await GetAGameWithRulesByIdAsync(gameToUpdate.GameId);
        
        // Update the game attributes
        if (!string.IsNullOrEmpty(gameToUpdate.Name))
        {
            existingGame.Name = gameToUpdate.Name;
        }

        if (!string.IsNullOrEmpty(gameToUpdate.Author))
        {
            existingGame.Author = gameToUpdate.Author;
        }

      

        // Update the rules
        if (gameToUpdate.Rules.Count > 0){
            for (int i = 0; i < existingGame.Rules.Count; i++)
            {
                if (gameToUpdate.Rules[i].DivisibleBy.HasValue)
                {
                    existingGame.Rules[i].DivisibleBy = gameToUpdate.Rules[i].DivisibleBy.Value;
                }

                if (!string.IsNullOrEmpty(gameToUpdate.Rules[i].ReplaceWith))
                {
                    existingGame.Rules[i].ReplaceWith = gameToUpdate.Rules[i].ReplaceWith;
                }
            }
        }

        // Save changes via the repository
        await _gameRepo.UpdateGameAsync(existingGame);
        
    }

    // remove a game with its rules
    public async Task RemoveGameWithRulesByIdAsync(Guid gameId)
    {
        var existingGame = await GetAGameWithRulesByIdAsync(gameId);
        
        // if (existingGame == null)
        // {
        //     throw new Exception($"Game with id {gameId} does not exist");
        // }

        if (existingGame != null)
        {
            // Remove rules using the RuleService
            await _ruleService.RemoveRulesByGameIdAsync(gameId);

            // Remove the game
            await _gameRepo.RemoveGameAsync(existingGame);
        }

    }
    
    
}