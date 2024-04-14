using Domain.Entities;

namespace Application.Customers.CreateCustomer;

public sealed record CreateCustomerRequest(
    string FirstName,
    string LastName,
    DateTime BirthDay
) : IRequest<int>;

public class CreateCustomerHandler(ITodoDbContext context) : IRequestHandler<CreateCustomerRequest, int>
{
    public async Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        Customer entity = new ()
        {
            Birthday = DateOnly.FromDateTime(request.BirthDay),
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        context.Customers.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}