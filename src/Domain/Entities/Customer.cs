namespace Domain.Entities;

public class Customer
{
    public int Id { get; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateOnly Birthday { get; set; }

}