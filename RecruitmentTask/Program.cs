using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentTask.Database.Persistence.Models;
using RecruitmentTask.Logic.Interfaces.Interfaces;
using RecruitmentTask.Logic.Logics;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();

var connString = config.GetConnectionString("RecruitmentTaskBaseConnection");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductLogic, ProductLogic>();

builder.Services.AddDbContext<RecruitmentTaskBaseContext>(options =>
    options.UseNpgsql(connString));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
