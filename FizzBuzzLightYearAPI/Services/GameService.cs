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
        var existingGame = await GetAGameWithRulesByIdAsync(gameId);
        
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
    public async Task AddGameWithRulesAsync(Game game, List<Rule> rules)
    {
        await _gameRepo.AddGameAsync(game);
        foreach (var rule in rules)
        {
            rule.GameId = game.GameId;
        }
        await _ruleService.AddRuleListAsync(rules);
    }
    
    // update a game with its rules
    public async Task UpdateGameWithRulesAsync(Game gameToUpdate)
    {
        var existingGame = await GetAGameWithRulesByIdAsync(gameToUpdate.GameId);
        // if (existingGame == null)
        // {
        //     throw new Exception($"Game with id {gameToUpdate.GameId} does not exist");
        // }
        
        // Update the game attributes
        existingGame.Name = gameToUpdate.Name;
        existingGame.Author = gameToUpdate.Author;

        // Update the rules
        for (int i = 0; i < existingGame.Rules.Count; i++)
        {
            existingGame.Rules[i].DivisibleBy = gameToUpdate.Rules[i].DivisibleBy;
            existingGame.Rules[i].ReplaceWith = gameToUpdate.Rules[i].ReplaceWith;
            
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