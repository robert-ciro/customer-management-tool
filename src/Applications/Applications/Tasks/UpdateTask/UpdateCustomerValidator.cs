namespace Application.Tasks.UpdateTask;

public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskValidator()
    {
        RuleFor(v => v.Description)
        .NotEmpty()
        .MaximumLength(1000);
    }
}
