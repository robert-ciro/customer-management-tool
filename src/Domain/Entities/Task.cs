namespace Domain.Entities;

public class Task
{
    public int Id { get; }
    public required string Description { get; set; }
    public required DateTime CreationDate { get; set; }
    public bool Solved { get; set; }
    public required Customer Customer { get; set; }
}
