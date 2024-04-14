namespace Application.Contacts.DeleteContact;

public sealed record DeleteContactRequest(
    int Id
) : IRequest;

public class DeleteContactHandler(ITodoDbContext context) : IRequestHandler<DeleteContactRequest>
{
    public async Task Handle(DeleteContactRequest request, CancellationToken cancellationToken)
    {
        var customer = await context.Contacts.FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, customer);

        context.Contacts.Remove(customer);

        await context.SaveChangesAsync(cancellationToken);
    }
}