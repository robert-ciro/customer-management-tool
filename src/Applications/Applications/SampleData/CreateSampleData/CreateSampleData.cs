using Domain.Entities;

namespace Application.SampleData.CreateSampleData;

public sealed record CreateSampleRequest() : IRequest;

public class CreateSampleHandler(ITodoDbContext context) : IRequestHandler<CreateSampleRequest>
{
    public async System.Threading.Tasks.Task Handle(CreateSampleRequest request, CancellationToken cancellationToken)
    {
        var customers = CreateCustomers(100000);

        await context.BulkInsertAsync(customers, cancellationToken);

        List<Contact> contacts = new(100_000 * 3);
        List<Domain.Entities.Task> tasks = new(100_000 * 10);

        foreach (Customer customer in customers)
        {
            contacts.AddRange(CreateContacts(numberOfContacts: 3, customer.Id));
            tasks.AddRange(CreateTasks(numberOfTasks: 10, customer.Id));
        }

        await context.BulkInsertAsync(contacts, cancellationToken);
        await context.BulkInsertAsync(tasks, cancellationToken);
    }

    private static List<Customer> CreateCustomers(int numberOfCustomers) => Enumerable.Range(1, numberOfCustomers).Select(i => new Customer
    {
        FirstName = "FirstName" + i,
        LastName = "LastName" + i,
        Birthday = new DateOnly(1990, 09, 26)
    }).ToList();

    private static List<Contact> CreateContacts(int numberOfContacts, int customerId) => Enumerable.Range(1, numberOfContacts).Select(i => new Contact
    {
        Customer = null!,
        CustomerId = customerId,
        Type = TypeEnum.Phone,
        Value = "+32611111111" + i
    }).ToList();


    private static List<Domain.Entities.Task> CreateTasks(int numberOfTasks, int customerId) => Enumerable.Range(1, numberOfTasks).Select(i => new Domain.Entities.Task
    {
        CustomerId = customerId,
        Customer = null!,
        Description = "Task" + i,
        CreationDate = DateTime.Now
    }).ToList();
}
