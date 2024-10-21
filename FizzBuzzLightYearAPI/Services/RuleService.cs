using FizzBuzzLightYearAPI.Models;
using FizzBuzzLightYearAPI.Repositories;

namespace FizzBuzzLightYearAPI.Services;

public class RuleService
{
    private readonly RuleRepository _ruleRepo;
    
    public RuleService(RuleRepository ruleRepository)
    {
        _ruleRepo = ruleRepository;
    }
    
    public async Task AddRuleListAsync(List<Rule> rules)
    {
        await _ruleRepo.AddRuleListAsync(rules);
    }

    public async Task RemoveRulesByGameIdAsync(Guid gameId)
    {
        await _ruleRepo.RemoveAllRulesByGameIdAsync(gameId);
    }

    public async Task<List<Rule>> GetRulesByGameIdAsync(Guid gameId)
    {
        return await _ruleRepo.GetRulesByGameIdAsync(gameId);
    }


}