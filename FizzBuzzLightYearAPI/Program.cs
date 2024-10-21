using FizzBuzzLightYearAPI.Context;
using FizzBuzzLightYearAPI.Repositories;
using FizzBuzzLightYearAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database context service
builder.Services.AddDbContext<APIDbContext>(options =>
{
    // Configure Entity Framework Core to connect to database
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddScoped<GameService>();
builder.Services.AddScoped<GameRepository>();

builder.Services.AddScoped<RuleService>();
builder.Services.AddScoped<RuleRepository>();
// builder.Services.AddScoped<GameSessionService>();
// builder.Services.AddScoped<QuestionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();