using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Application.Contacts.UpdateContact;

public sealed record UpdateContactRequest(
    int Id,
    string Value,
    int CustomerId
) : IRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeEnum Type { get; init; }
}

public class UpdateContactHandler(ITodoDbContext context) : IRequestHandler<UpdateContactRequest>
{
    public async Task Handle(UpdateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = await context.Contacts
                                    .Where(c => c.Id == request.Id)
                                   .Include(c => c.Customer)
                                   .FirstOrDefaultAsync();
                                  
        Guard.Against.NotFound(request.Id, contact);

        var customer = await context.Customers.FindAsync(request.CustomerId, cancellationToken);
        Guard.Against.NotFound(request.CustomerId, customer);

        contact.Value = request.Value;
        contact.Type = request.Type;
        contact.Customer = customer;

        await context.SaveChangesAsync(cancellationToken);
    }
}