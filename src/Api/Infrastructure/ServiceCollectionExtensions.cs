using Ardalis.GuardClauses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services) => services.AddProblemDetails(setup =>
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
            else if (context.Exception is NotFoundException)
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
}