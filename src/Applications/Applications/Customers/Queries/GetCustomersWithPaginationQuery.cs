using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Application.Customers.Queries;

public record GetCustomersWithPaginationQuery(
    string? FirstName = null,
    string? LastName = null,
    int PageNumber = 1,
    int PageSize = 20) : IRequest<ImmutableArray<CustomerResponse>>;

public class GetCustomersWithPaginationQueryHandler(ITodoDbContext context) : IRequestHandler<GetCustomersWithPaginationQuery, ImmutableArray<CustomerResponse>>
{
    public Task<ImmutableArray<CustomerResponse>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken) =>
        System.Threading.Tasks.Task.Run(() =>
        {
            IQueryable<Customer> query = context.Customers.AsQueryable();

            if (request.FirstName != null)
                query = query.Where(c => EF.Functions.Like(c.FirstName, $"%{request.FirstName}%"));

            if (request.LastName != null)
                query = query.Where(c => EF.Functions.Like(c.LastName, $"%{request.LastName}%"));

            return query
                .OrderBy(x => x.FirstName)
                .Select(c => new CustomerResponse(c.Id, c.FirstName, c.LastName, c.Birthday))
                .Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize)
                .AsNoTracking()
                .ToImmutableArray();
        });
}
