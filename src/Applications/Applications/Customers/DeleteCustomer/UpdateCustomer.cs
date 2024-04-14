using Microsoft.EntityFrameworkCore;

namespace Application.Customers.DeleteCustomer;

public sealed record DeleteCustomerRequest(
    int Id
) : IRequest;

public class CreateTodoListCommandHandler(ITodoDbContext context) : IRequestHandler<DeleteCustomerRequest>
{
    public async Task Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, customer);

        context.Customers.Remove(customer);

        await context.SaveChangesAsync(cancellationToken);
    }
}