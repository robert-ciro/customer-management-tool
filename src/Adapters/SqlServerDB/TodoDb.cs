using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adapters.SqlServerDB;

public class TodoDb : DbContext, ITodoDbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Domain.Entities.Task> Tasks => Set<Domain.Entities.Task>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Customer>()
                    .HasKey(c => c.Id);

        modelBuilder.Entity<Contact>()
                   .HasKey(c => c.Id);

        modelBuilder.Entity<Contact>()
                    .HasOne(x => x.Customer);

        modelBuilder.Entity<Contact>()
                    .Property(e => e.Type)
                    .HasConversion<string>();

        modelBuilder.Entity<Domain.Entities.Task>()
                   .HasKey(c => c.Id);

        modelBuilder.Entity<Domain.Entities.Task>()
                    .HasOne(x => x.Customer);
    }
}

