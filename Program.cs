using CalorieTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;
using CalorieTrackerAPI.Controllers;
using CalorieTrackerAPI.Repositories;
using CalorieTrackerAPI.Services;
using CalorieTrackerWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CalorieTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUserRepo<User>, UserRepo>();
builder.Services.AddTransient<IUserService<User>, UserService>();
builder.Services.AddTransient<IEntryRepo<FoodEntry>, FoodEntryRepo>();
builder.Services.AddTransient<IEntryRepo<Workout>, WorkoutRepo>();
builder.Services.AddTransient<IEntryService<FoodEntry>, FoodEntryService>();
builder.Services.AddTransient<IEntryService<Workout>, WorkoutService>();
builder.Logging.AddLog4Net();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
