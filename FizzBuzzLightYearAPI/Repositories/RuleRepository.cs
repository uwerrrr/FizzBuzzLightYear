using FizzBuzzLightYearAPI.Context;
using FizzBuzzLightYearAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzLightYearAPI.Repositories;

public class RuleRepository
{
    private readonly APIDbContext _context;

    public RuleRepository(APIDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Rule>> GetRulesByGameIdAsync(Guid gameId)
    {
        return await _context.Rules
            .Where(a => a.GameId == gameId)
            .ToListAsync();
    }
    
    
    public async Task AddRuleListAsync(List<Rule> rules)
    {
        _context.Rules.AddRange(rules);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task AddARuleAsync(Rule rule)
    {
        await _context.Rules.AddAsync(rule);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveAllRulesByGameIdAsync(Guid gameId)
    {
        _context.Rules
            .RemoveRange(_context.Rules
                .Where(a => a.GameId == gameId));
        await _context.SaveChangesAsync();
    }
    
    
}