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

    public System.Threading.Tasks.Task BulkInsertAsync<T>(IEnumerable<T> entities, CancellationToken cancellationToken) where T : class
        => DbContextExtensions.BulkInsertAsync(this, entities, cancellationToken);

    public System.Threading.Tasks.Task RemoveAllData(CancellationToken cancellationToken)
        => Database.ExecuteSqlRawAsync(@"
ALTER TABLE Contacts DROP CONSTRAINT FK_Customers_Contacts;
ALTER TABLE Tasks DROP CONSTRAINT FK_Customers_Tasks;

TRUNCATE TABLE Contacts;
TRUNCATE TABLE Tasks;
TRUNCATE TABLE Customers;

ALTER TABLE Contacts ADD CONSTRAINT FK_Customers_Contacts FOREIGN KEY (CustomerID) REFERENCES Customers(Id);
ALTER TABLE Tasks ADD CONSTRAINT FK_Customers_Tasks FOREIGN KEY (CustomerID) REFERENCES Customers(Id);",
            cancellationToken);

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

