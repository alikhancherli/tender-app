using System.Linq.Expressions;
using Tender.App.Domain.Entities;

namespace Tender.App.Domain.Repositories;

public interface IBidRepository
{
    ValueTask<Bid?> GetAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken);
    ValueTask<Bid?> GetAsync(int id, CancellationToken cancellationToken);
    Task<IList<Bid>> GetListAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Bid bid);
    ValueTask<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    ValueTask<bool> DeleteAsync(Bid bid, CancellationToken cancellationToken);
    Task AddAsync(Bid bid);
    Task SaveAndDispatchEventsAsync(CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
