using Microsoft.EntityFrameworkCore;
using Nexus.Application.Services.Implementations;
using Nexus.Application.Services.Interfaces;
using Nexus.Infrastructure.Context;
using Nexus.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<NexusContext>(ops => ops.UseSqlServer(builder.Configuration.GetConnectionString("NexusConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserService,UserService>();

// Thêm AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// builder.Services.AddCors(options =>
// {
//     options.AddDefaultPolicy(builder =>
//     {
//         builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//     });
// });

var app = builder.Build();

//app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
