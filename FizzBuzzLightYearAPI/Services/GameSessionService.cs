using System.Text;
using FizzBuzzLightYearAPI.Context;
using FizzBuzzLightYearAPI.DTOs;
using FizzBuzzLightYearAPI.DTOs.GameSession;
using FizzBuzzLightYearAPI.DTOs.Question;
using FizzBuzzLightYearAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzLightYearAPI.Services;

public class GameSessionService
{
    private readonly APIDbContext _context;
    private readonly GameService _gameService;
    
    private readonly Random _random;
    private const int MIN_NUMBER = 1;
    private const int MAX_NUMBER = 1000;
    private const int POINTS_PER_CORRECT_ANSWER = 1;
    

    public GameSessionService(APIDbContext context, GameService gameService)
    {
        _context = context;
        _gameService = gameService;
    }
    
    public async Task<GameSessionResponseDTO> StartNewSessionAsync(StartGameSessionDTO request)
    {
        var game = await _gameService.GetAGameWithRulesByIdAsync(request.GameId);
        
        var newGameSession = new GameSession
        {
            GameId = game.GameId,
            StartTime = DateTime.UtcNow,
            DurationSeconds = request.DurationSeconds,
            EndTime = DateTime.UtcNow.AddSeconds(request.DurationSeconds),
            IsActive = true
        };

        _context.GameSessions.Add(newGameSession);
        await _context.SaveChangesAsync();

        // Generate first question
        var firstQuestion = await GenerateQuestionAsync(newGameSession);

        // will do mapper here
        return new GameSessionResponseDTO
        {
            SessionId = newGameSession.SessionId,
            Rules = game.Rules.Select(r => new RuleDTO 
            {
                DivisibleBy = r.DivisibleBy,
                ReplaceWith = r.ReplaceWith
            }).ToList(),
            StartTime = newGameSession.StartTime,
            EndTime = newGameSession.EndTime,
            IsActive = true,
            CurrentQuestion = new QuestionDTO
            {
                QuestionId = firstQuestion.QuestionId,
                Number = firstQuestion.Number
            }
        };
    }
    
    
    // public async Task<GameSessionStatsDTO> GetSessionStatsAsync(Guid sessionId)
    // {
    //     var session = await _context.GameSessions
    //         .FirstOrDefaultAsync(s => s.SessionId == sessionId);
    //
    //     if (session == null)
    //         throw new NotFoundException("Session not found");
    //
    //     return new GameSessionStatsDto
    //     {
    //         CorrectAnswers = session.CorrectAnswers,
    //         IncorrectAnswers = session.IncorrectAnswers
    //     };
    // }
    //
    
    ///////// private methods
    
    private async Task<Question> GenerateQuestionAsync(GameSession session)
    {
        
        // will put this in Question service & repo
        // get list of used numbers in the game session
        var usedNumbers = await _context.Questions
            .Where(q => q.SessionId == session.SessionId)
            .Select(q => q.Number)
            .ToListAsync();

        // generate random number
        int number = _random.Next(MIN_NUMBER, MAX_NUMBER + 1);

        // Continue generating numbers until a unique one is found
        while (usedNumbers.Contains(number))
        {
            number = _random.Next(MIN_NUMBER, MAX_NUMBER + 1);
        }

        var question = new Question
        {
            SessionId = session.SessionId,
            Number = number,
            ExpectedAnswer = CalculateAnswer(number, session.Game.Rules),
            GeneratedAt = DateTime.UtcNow
        };

        // will put this in repo
        _context.Questions.Add(question);
        await _context.SaveChangesAsync();

        return question;
    }
    
    
    private string CalculateAnswer(int number, List<Rule> rules)
    {
        var result = new StringBuilder();
        var hasMatch = false;

        foreach (var rule in rules.OrderBy(r => r.DivisibleBy))
        {
            if (number % rule.DivisibleBy == 0)
            {
                result.Append(rule.ReplaceWith);
                hasMatch = true;
            }
        }

        return hasMatch ? result.ToString() : number.ToString();
    }
}