using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.UpdateTask;

public sealed record UpdateTaskRequest(
    int Id,
    string Description,
    int CustomerId,
    bool Solved
) : IRequest;

public class UpdateTaskHandler(ITodoDbContext context) : IRequestHandler<UpdateTaskRequest>
{
    public async System.Threading.Tasks.Task Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks
                                    .Where(c => c.Id == request.Id)
                                   .Include(c => c.Customer)
                                   .FirstOrDefaultAsync();

        Guard.Against.NotFound(request.Id, task);

        if(task.Customer.Id != request.CustomerId)
        {
            var customer = await context.Customers.FindAsync(request.CustomerId, cancellationToken);
            Guard.Against.NotFound(request.CustomerId, customer);
            task.Customer = customer;
        }

        task.Description = request.Description;
        task.Solved = request.Solved;

        await context.SaveChangesAsync(cancellationToken);
    }
}