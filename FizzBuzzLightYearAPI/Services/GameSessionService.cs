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
    
    private readonly Random _random = new Random();
    private const int MIN_NUMBER = 1;
    private const int MAX_NUMBER = 1000;

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
            IsActive = true,
            Player = request.PlayerName
        };

        _context.GameSessions.Add(newGameSession);
        await _context.SaveChangesAsync();

        // Generate first question
        var firstQuestion = await GenerateQuestionAsync(newGameSession);

        // TODO: mapper here
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
    
    
    public async Task<AnswerResponseDTO> ProcessAnswerAsync(SubmitAnswerDTO submittedAnswer)
    {
        // get session data from db
        var session = await _context.GameSessions
            .Include(s => s.Game)
            .ThenInclude(g => g.Rules)
            .Include(s => s.Questions)
            .FirstOrDefaultAsync(s => s.SessionId == submittedAnswer.SessionId);

        if (session == null)
            throw new Exception("Session not found");

        var question = session.Questions.FirstOrDefault(q => q.QuestionId == submittedAnswer.QuestionId);


        if (question == null)
            throw new Exception("Question not found");
        

        // Update question with user's submittedAnswer
        question.PlayerAnswer = submittedAnswer.PlayerAnswer;
        
        // check if player answer correct
        question.IsCorrect = string.Equals(
            submittedAnswer.PlayerAnswer.Trim(), 
            question.ExpectedAnswer.Trim(), 
            StringComparison.OrdinalIgnoreCase);

        if (question.IsCorrect == true)
            session.CorrectAnswerNum++;
        else
            session.IncorrectAnswerNum++;

        // Generate next question if game is still active
        Question? nextQuestion = null;
        if (DateTime.UtcNow < session.EndTime)
        {
            nextQuestion = await GenerateQuestionAsync(session);
        }
        else
        {
            session.IsActive = false;
        }

        // if (!session.IsActive || DateTime.UtcNow > session.EndTime)
        // {
        //     session.IsActive = false;
        //     await _context.SaveChangesAsync();
        //     return new AnswerResponseDTO
        //     {
        //         GameEnded = true
        //     };
        // }
        
        await _context.SaveChangesAsync();

        return new AnswerResponseDTO
        {
            IsCorrect = question.IsCorrect ?? false,
            CorrectAnswer = question.ExpectedAnswer,
            PlayerAnswer = submittedAnswer.PlayerAnswer,
            NextQuestion = nextQuestion != null ? new QuestionDTO
            {
                QuestionId = nextQuestion.QuestionId,
                Number = nextQuestion.Number
            } : null,
            GameEnded = !session.IsActive,
        };
    }
    
    
    public async Task<GameSessionStatsDTO> GetSessionStatsAsync(Guid sessionId)
    {
        var session = await GetGameSessionById(sessionId);

        // TODO: mapping
        return new GameSessionStatsDTO
        {
            CorrectAnswerNum = session.CorrectAnswerNum,
            IncorrectAnswerNum = session.IncorrectAnswerNum
        };
    }
    
    ///////// private methods
    
    private async Task<GameSession> GetGameSessionById(Guid sessionId)
    {
        // TODO: put this in repo
        var session = await _context.GameSessions
            .FirstOrDefaultAsync(s => s.SessionId == sessionId);

        if (session == null)
            throw new Exception("Session not found");
        return session;
    }

    
    private async Task<Question> GenerateQuestionAsync(GameSession session)
    {
        
        // TODO: put this in Question service & repo
        // get list of used numbers in the game session
        var usedNumbers = await _context.Questions
            .Where(q => q.SessionId == session.SessionId)
            .Select(q => q.Number)
            .ToListAsync();

        // generate random number
        var number = _random.Next(MIN_NUMBER, MAX_NUMBER + 1);

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

        // TODO: put this in repo
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