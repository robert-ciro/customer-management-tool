namespace Application.Tasks.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskValidator()
    {
        RuleFor(v => v.Description)
            .NotEmpty()
            .MaximumLength(1000);
    }
}
