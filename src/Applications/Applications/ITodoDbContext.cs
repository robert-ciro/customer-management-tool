using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface ITodoDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Contact> Contacts { get; }
    DbSet<Domain.Entities.Task> Tasks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}