using FizzBuzzLightYearAPI.Context;
using FizzBuzzLightYearAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzLightYearAPI.Repositories;

public class GameRepository
{
    private readonly APIDbContext _context;

    public GameRepository(APIDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Game>> GetAllGamesWithRulesAsync()
    {
        return await _context.Games
            .Include(q => q.Rules)
            .ToListAsync();
    }
    
    public async Task<Game> GetAGameWithRulesByIdAsync(Guid gameId)
    {
        return await _context.Games
            .Include(q => q.Rules)
            .FirstOrDefaultAsync(g => g.GameId == gameId);
    }
    
    public async Task<bool> IsNameUniqueAsync(string name)
    {
        return !await _context.Games
            .AnyAsync(g => g.Name.ToLower() == name.ToLower());
    }
    
    public async Task<bool> IsGameExistsAsync(Guid gameId)
    {
        return await _context.Games.AnyAsync(q => q.GameId == gameId);
    }
    
    
    public async Task AddGameAsync(Game game) // Game includes list of rules
    {
        _context.Games.Add(game);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateGameAsync(Game game) // Game includes list of rules
    {
        _context.Update(game);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveGameAsync(Game game)
    {
        _context.Games.Remove(game);
        await _context.SaveChangesAsync();
    }
    
}