using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Contacts.CreateContact;

public record CreateContactRequest(
    string Value,
    int CustomerId
) : IRequest<int>
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeEnum Type { get; init; }
}

public class CreateContactCommandHandler(ITodoDbContext context) : IRequestHandler<CreateContactRequest, int>
{
    public async Task<int> Handle(CreateContactRequest request, CancellationToken cancellationToken)
    {
        Customer? customer = await context.Customers.FindAsync(request.CustomerId, cancellationToken);

        Guard.Against.NotFound(request.CustomerId, customer);

        Contact entity = new()
        {
            Type = request.Type,
            Customer = customer,
            Value = request.Value,
        };

        context.Contacts.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}