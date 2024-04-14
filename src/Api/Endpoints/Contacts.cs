using Api.Infrastructure;
using Application.Contacts.CreateContact;
using Application.Customers.DeleteCustomer;
using Application.Customers.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

public class Contacts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this)
            .MapPost(CreateContact);
    }

    public static Task<int> CreateContact(
        [FromBody] CreateContactRequest request,
        [FromServices] ISender sender,
        CancellationToken token) => sender.Send(request, token);
}