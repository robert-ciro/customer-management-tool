using Api.Infrastructure;
using Application.Contacts.CreateContact;
using Application.Contacts.DeleteContact;
using Application.Contacts.UpdateContact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public class Contacts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .MapPost(CreateContact)
            .MapPut(UpdateContact, "{id}")
            .MapDelete(DeleteContact, "{id}");
    }

    public static Task<int> CreateContact(
        [FromBody] CreateContactRequest request,
        [FromServices] ISender sender,
        CancellationToken token) => sender.Send(request, token);

    public static async Task<IResult> UpdateContact(
        [FromRoute] int id,
        [FromBody] UpdateContactRequest request,
        [FromServices] ISender sender,
        CancellationToken token)
    {
        if (id != request.Id) return Results.BadRequest();

        await sender.Send(request, token);

        return Results.NoContent();
    }

    public static async Task<IResult> DeleteContact(
     [FromRoute] int id,
     [FromServices] ISender sender,
     CancellationToken token)
    {
        await sender.Send(new DeleteContactRequest(id), token);

        return Results.NoContent();
    }
}