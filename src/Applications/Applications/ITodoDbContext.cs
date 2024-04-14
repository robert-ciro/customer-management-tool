using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface ITodoDbContext
{
    DbSet<Customer> Customers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}