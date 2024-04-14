namespace Application.Customers.Queries;

public record CustomerResponse(int Id, string FirstName, string LastName, DateOnly Birthday);