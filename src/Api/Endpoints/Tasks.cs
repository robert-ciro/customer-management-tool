using Api.Infrastructure;
using Application.Tasks.CreateTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public class Tasks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .MapPost(CreateTask);
    }

    public static Task<int> CreateTask(
        [FromBody] CreateTaskRequest request,
        [FromServices] ISender sender,
        CancellationToken token) => sender.Send(request, token);
}