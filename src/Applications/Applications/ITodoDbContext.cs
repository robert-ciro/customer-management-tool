using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface ITodoDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Contact> Contacts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}