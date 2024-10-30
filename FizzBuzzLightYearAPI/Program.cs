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

builder.Services.AddScoped<GameSessionService>();
// builder.Services.AddScoped<QuestionService>();



// CORS policy setup - for local frontend access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000","http://localhost:3001","http://localhost:3002") // Specify the client origin
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Database initialization
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<APIDbContext>();
        
        // Ensure database is created and apply migrations
        context.Database.EnsureCreated();
        
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
        
        // Verify if seeding is needed
        if (!context.Games.Any())
        {
            // The seeding is handled in OnModelCreating
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while initializing the database.");
        throw;
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Enable CORS
app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();