namespace Application.Tasks.MarkTaskStatus;

public enum StateTask
{
    Solve = 1,
    Unresolved = 2
}

public record MarkTaskStatusRequest(
    StateTask State,
    int Id
) : IRequest;


public class MarkTaskStatusTaskHandler(ITodoDbContext context) : IRequestHandler<MarkTaskStatusRequest>
{
    public async Task Handle(MarkTaskStatusRequest request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, task);

        task.Solved = request.State == StateTask.Solve;

        await context.SaveChangesAsync(cancellationToken);
    }
}