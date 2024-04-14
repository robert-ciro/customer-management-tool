using Api.Infrastructure;
using Application.Customers.CreateCustomer;
using Application.Customers.DeleteCustomer;
using Application.Customers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .MapPost(CreateCustomer)
            .MapPut(UpdateCustomer, "{id}")
            .MapDelete(DeleteCustomer, "{id}");
    }

    public static Task<int> CreateCustomer(
        [FromBody] CreateCustomerRequest request,
        [FromServices] ISender sender,
        CancellationToken token) => sender.Send(request, token);

    public static async Task<IResult> UpdateCustomer(
        [FromRoute] int id,
        [FromBody] UpdateCustomerRequest request,
        [FromServices] ISender sender,
        CancellationToken token)
    {
        if (id != request.Id) return Results.BadRequest();

        await sender.Send(request, token);

        return Results.NoContent();
    }

    public static async Task<IResult> DeleteCustomer(
     [FromRoute] int id,
     [FromServices] ISender sender,
     CancellationToken token)
    {
        await sender.Send(new DeleteCustomerRequest(id), token);

        return Results.NoContent();
    }
}