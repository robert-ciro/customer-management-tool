using Api.Infrastructure;
using Application.Tasks.MarkTaskStatus;
using Application.Tasks.CreateTask;
using Application.Tasks.DeleteTaks;
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
            .MapPut(UpdateTask, "{id:int}")
            .MapPost(MarkTaskStatus, "{id:int}/{state:alpha}");
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

    public static async Task<IResult> DeleteTask(
     [FromRoute] int id,
     [FromServices] ISender sender,
     CancellationToken token)
    {
        await sender.Send(new DeleteTaskRequest(id), token);

        return Results.NoContent();
    }

    public static async Task<IResult> MarkTaskStatus(
     [FromRoute] int id,
     [FromRoute] StateTask state,
     [FromServices] ISender sender,
      CancellationToken token)
    {
        await sender.Send(new MarkTaskStatusRequest(state, id), token);
        return Results.NoContent();
    }
}