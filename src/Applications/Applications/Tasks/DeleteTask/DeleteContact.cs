namespace Application.Tasks.DeleteTaks;

public sealed record DeleteTaskRequest(
    int Id
) : IRequest;

public class DeleteContactHandler(ITodoDbContext context) : IRequestHandler<DeleteTaskRequest>
{
    public async Task Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await context.Tasks.FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, task);

        context.Tasks.Remove(task);

        await context.SaveChangesAsync(cancellationToken);
    }
}