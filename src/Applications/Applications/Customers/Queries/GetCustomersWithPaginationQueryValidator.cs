namespace Application.Customers.Queries;

public class GetCustomersWithPaginationQueryValidator : AbstractValidator<GetCustomersWithPaginationQuery>
{
    public GetCustomersWithPaginationQueryValidator()
    {
        RuleFor(v => v.FirstName)
           .MaximumLength(60);

        RuleFor(v => v.LastName)
            .MaximumLength(60);
    }
}