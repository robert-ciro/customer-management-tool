using Adapters.SqlServerDB;
using Api.Infrastructure;
using Application;
using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoListDatabase")))
                .AddScoped<ITodoDbContext>(provider => provider.GetRequiredService<TodoDb>());

builder.Services.AddApplicationServices()
                .AddCustomProblemDetails();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(conf => conf.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       );

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseExceptionHandler();

app.MapEndpoints();

app.Run();
