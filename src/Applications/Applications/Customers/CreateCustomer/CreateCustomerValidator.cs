namespace Application.Customers.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    public readonly static int MAX_AGE = 120;
    public readonly static int MIN_AGE = 6;

    public CreateCustomerValidator()
    {
        RuleFor(v => v.FirstName)
            .MaximumLength(60)
            .NotEmpty();

        RuleFor(v => v.LastName)
            .MaximumLength(60)
            .NotEmpty();

        RuleFor(v => v.BirthDay)
            .Custom((birthDay, context) =>
            {
                DateTime currentDateTime = DateTime.UtcNow;
                var age = currentDateTime.Year - birthDay.Year;

                if (age <= MIN_AGE)
                    context.AddFailure($"The min age is {MIN_AGE}");
                else if (age >= MAX_AGE)
                    context.AddFailure($"The max age is {MAX_AGE}");
            });
    }
}
