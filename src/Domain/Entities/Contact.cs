namespace Domain.Entities;


public enum TypeEnum
{
    Phone = 1,
    Email = 2,
    Web = 3
}
public class Contact
{
    public int Id { get; }
    public required TypeEnum Type { get; set; }
    public required string Value { get; set; }
    public required Customer Customer { get; set; }
    public int CustomerId { get; set; }
}
