using Domain.Entities;

namespace Application.Tasks.CreateTask;

public record CreateTaskRequest(
    string Description,
    int CustomerId
) : IRequest<int>;

public class CreateTaskHandler(ITodoDbContext context) : IRequestHandler<CreateTaskRequest, int>
{
    public async Task<int> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        Customer? customer = await context.Customers.FindAsync(request.CustomerId, cancellationToken);

        Guard.Against.NotFound(request.CustomerId, customer);

        Domain.Entities.Task entity = new()
        {
            CreationDate = DateTime.UtcNow,
            Customer = customer,
            Description = request.Description,
        };

        context.Tasks.Add(entity);
        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}