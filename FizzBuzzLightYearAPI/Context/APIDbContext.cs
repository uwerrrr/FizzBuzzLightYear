using FizzBuzzLightYearAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FizzBuzzLightYearAPI.Context;

public class APIDbContext: DbContext
{
    // Constructor
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) {}
    
    // Enities
    public DbSet<Game> Games { get; set; }
    public DbSet<Rule> Rules { get; set; }
    public DbSet<GameSession> GameSessions { get; set; }
    public DbSet<Question> Questions { get; set; }
    
    // Configure the model creation and seed initial data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
       
        var fizzBuzzLooGameId = new Guid("11111111-1111-1111-1111-111111111111");
        var fooBooLooGameId = new Guid("22222222-2222-2222-2222-222222222222");
        var seedGameSessionId = new Guid("33333333-3333-3333-3333-333333333333");
        
        // Seed initial data for Game
        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                GameId = fizzBuzzLooGameId,
                Name = "FizzBuzz",
                Author = "Alex",
                CreatedDate = DateTime.UtcNow
            },
            new Game
            {
                GameId = fooBooLooGameId,
                Name = "FooBooLoo",
                Author = "John",
                CreatedDate = DateTime.UtcNow
            }
        );
        
        // Seed initial data for Rules
        modelBuilder.Entity<Rule>().HasData(
            // FizzBuzz Rules
            new Rule
            {
                RuleId = Guid.NewGuid(),
                GameId = fizzBuzzLooGameId,
                DivisibleBy = 3,
                ReplaceWith = "Fizz"
            },
            new Rule
            {
                RuleId = Guid.NewGuid(),
                GameId = fizzBuzzLooGameId,
                DivisibleBy = 5,
                ReplaceWith = "Buzz"
            },
            new Rule
            {
                RuleId = Guid.NewGuid(),
                GameId = fizzBuzzLooGameId,
                DivisibleBy = 8,
                ReplaceWith = "Loo"
            },
            // FooBooLoo Rules
            new Rule
            {
                RuleId = Guid.NewGuid(),
                GameId = fooBooLooGameId,
                DivisibleBy = 7,
                ReplaceWith = "Foo"
            },
            new Rule
            {
                RuleId = Guid.NewGuid(),
                GameId = fooBooLooGameId,
                DivisibleBy = 11,
                ReplaceWith = "Boo"
            },
            new Rule
            {
                RuleId = Guid.NewGuid(),
                GameId = fooBooLooGameId,
                DivisibleBy = 103,
                ReplaceWith = "Loo"
            }
        );
        
        // Seed initial data for GameSessions
        modelBuilder.Entity<GameSession>().HasData(
            new GameSession
            {
                SessionId = seedGameSessionId,
                GameId = fizzBuzzLooGameId,
                Player = "TestPlayer",
                DurationSeconds = 60,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddSeconds(60),
                IsActive = false,
                CorrectAnswerNum = 3,
                IncorrectAnswerNum = 2
            }
        );
        
        
        //  // Seed initial data for Questions
        //  modelBuilder.Entity<Question>().HasData(
        //      new Question
        //      {
        //          QuestionId = Guid.NewGuid(),
        //          SessionId = seedGameSessionId, 
        //          Number = 3,
        //          PlayerAnswer = "Fizz", 
        //          IsCorrect = true
        //      },
        //      new Question
        //      {
        //          QuestionId = Guid.NewGuid(),
        //          SessionId = seedGameSessionId,
        //          Number = 5,
        //          PlayerAnswer = "Buzz", 
        //          IsCorrect = true
        //      },
        //      new Question
        //      {
        //          QuestionId = Guid.NewGuid(),
        //          SessionId = seedGameSessionId,
        //          Number = 8,
        //          PlayerAnswer = "Fizz", 
        //          IsCorrect = false
        //      },
        //      new Question
        //      {
        //          QuestionId = Guid.NewGuid(),
        //          SessionId = seedGameSessionId,
        //          Number = 15,
        //          PlayerAnswer = "FizzBuzz", 
        //          IsCorrect = true
        //      }
        // );

    }

}