using Adapters.SqlServerDB;
using Api.Infrastructure;
using Application.SampleData.CreateSampleData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public class SampleData : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/sample-data")
            .WithTags(nameof(SampleData))
            .WithOpenApi()
            .MapPost(GenerateSampleData)
            .MapDelete(DeleteAllData);
    }

    public static async Task<IResult> GenerateSampleData(
        [FromServices] ISender sender,
        [FromServices] TodoDb context,
        CancellationToken token)
    {
        using (var transaction = await context.Database.BeginTransactionAsync())
        {
            await sender.Send(new CreateSampleRequest());
            transaction.Commit();
        }
        return Results.NoContent();
    }

    public static async Task<IResult> DeleteAllData(
     [FromServices] TodoDb context,
     CancellationToken token)
    {
        using (var transaction = await context.Database.BeginTransactionAsync())
        {
            await context.RemoveAllData(token);
            transaction.Commit();
        }
        return Results.NoContent();
    }
}