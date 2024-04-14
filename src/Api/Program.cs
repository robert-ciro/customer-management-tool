using Adapters.SqlServerDB;
using Api.Infrastructure;
using Application;
using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoDb>(options => options.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=ToDoList;Integrated Security=True"))
                .AddScoped<ITodoDbContext>(provider => provider.GetRequiredService<TodoDb>());

builder.Services
                .AddApplicationServices()
                .AddProblemDetails(setup =>
                {
                    setup.CustomizeProblemDetails = (context =>
                    {
                        if (context.Exception is ValidationException)
                        {
                            var exception = (ValidationException)context.Exception;
                            var erros = exception.Errors.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                                                        .ToDictionary(
                                   failureGroup => failureGroup.Key,
                                   failureGroup => failureGroup.ToArray());

                            context.ProblemDetails = new ValidationProblemDetails(erros)
                            {
                                Status = StatusCodes.Status400BadRequest,
                                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                            };
                        }

                        if (context.Exception is NotFoundException)
                        {
                            context.ProblemDetails = new()
                            {
                                Status = StatusCodes.Status404NotFound,
                                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                                Title = "The specified resource was not found.",
                                Detail = context.Exception.Message
                            };
                        }
                    });
                });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
    app.UseExceptionHandler();
else
    app.UseExceptionHandler();

app.MapEndpoints();

app.Run();
