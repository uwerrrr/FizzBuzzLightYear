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
    
    public async Task<bool> IsGameExistsAsync(Guid gameId)
    {
        return await _context.Games.AnyAsync(q => q.GameId == gameId);
    }
    
    public async Task AddGameAsync(Game Game)
    {
        _context.Games.Add(Game);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateGameAsync(Game Game)
    {
        _context.Update(Game);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveGameAsync(Game Game)
    {
        _context.Games.Remove(Game);
        await _context.SaveChangesAsync();
    }
    
}