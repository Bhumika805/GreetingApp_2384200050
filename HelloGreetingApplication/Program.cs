using BusinessLayer.Interface;
using BusinessLayer.Service;
using Microsoft.EntityFrameworkCore;
using NLog;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<GreetingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repository and business layer services
builder.Services.AddScoped<IGreetingRL, GreetingRL>();  // Add this line
builder.Services.AddScoped<IGreetingBL, GreetingBL>();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
logger.Info("Application is starting...");
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
