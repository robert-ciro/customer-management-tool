using Api.Infrastructure;
using Application.Tasks.CreateTask;
using Application.Tasks.UpdateTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public class Tasks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .MapPost(CreateTask)
            .MapPut(UpdateTask, "{id}");
    }

    public static Task<int> CreateTask(
        [FromBody] CreateTaskRequest request,
        [FromServices] ISender sender,
        CancellationToken token) => sender.Send(request, token);

    public static async Task<IResult> UpdateTask(
       [FromRoute] int id,
       [FromBody] UpdateTaskRequest request,
       [FromServices] ISender sender,
       CancellationToken token)
    {
        if (id != request.Id) return Results.BadRequest();

        await sender.Send(request, token);

        return Results.NoContent();
    }
}