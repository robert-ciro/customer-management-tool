namespace Application.Customers.UpdateCustomer;

public sealed record UpdateCustomerRequest(
    int Id,
    string FirstName,
    string LastName,
    DateTime BirthDay
) : IRequest;

public class CreateTodoListCommandHandler(ITodoDbContext context) : IRequestHandler<UpdateCustomerRequest>
{
    public async Task Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await context.Customers.FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, customer);

        customer.FirstName = request.FirstName;
        customer.LastName = request.LastName;
        customer.Birthday = DateOnly.FromDateTime(request.BirthDay);
  
        await context.SaveChangesAsync(cancellationToken);
    }
}