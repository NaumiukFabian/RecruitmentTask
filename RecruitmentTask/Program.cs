using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Database.Persistence.Models;
using RecruitmentTask.Logic.Interfaces.Interfaces;
using RecruitmentTask.Logic.Logics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductLogic, ProductLogic>();

builder.Services.AddDbContext<RecruitmentTaskBaseContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
