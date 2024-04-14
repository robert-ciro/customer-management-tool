using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adapters.SqlServerDB;

public class TodoDb : DbContext, ITodoDbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>()
                    .HasKey(c => c.Id);
    }
}

