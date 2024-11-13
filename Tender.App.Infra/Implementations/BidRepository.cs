using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Tender.App.Domain.Entities;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;
using Tender.App.Infra.Persistence;

namespace Tender.App.Infra.Implementations;

public sealed class BidRepository(AppDbContext _context, IMediator _mediator) : IBidRepository
{
    public async Task AddAsync(Bid bid)
    {
        await _context.Bids.AddAsync(bid);
    }

    public async ValueTask<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var bid = await GetAsync(id, cancellationToken);
        if (bid is null) return false;

        _context.Bids.Remove(bid);
        return true;
    }

    public async ValueTask<bool> DeleteAsync(Bid bid, CancellationToken cancellationToken)
    {
        _context.Bids.Remove(bid);
        return await Task.FromResult(true);
    }

    public async ValueTask<Bid?> GetAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Bids.AsNoTracking().Include(x => x.BidDetails).FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async ValueTask<Bid?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Bids.AsNoTracking().Include(x => x.BidDetails).FirstOrDefaultAsync(_ => _.Id == id, cancellationToken);
    }

    public async Task<IList<Bid>> GetListAsync(Expression<Func<Bid, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Bids.AsNoTracking().Include(x => x.BidDetails).Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task SaveAndDispatchEventsAsync(CancellationToken cancellationToken)
    {
        await SaveAsync(cancellationToken);
        IList<EntityEntry<IDomainEvent>> entryList = (from x in _context.ChangeTracker.Entries<IDomainEvent>()
                                                      where x.Entity.Events.Any()
                                                      select x).ToList();

        List<IDomainEventFlag> source = entryList.SelectMany((x) => x.Entity.Events).ToList();

        foreach (var sourceEvent in source)
            await _mediator.Publish(sourceEvent, cancellationToken);

        foreach (EntityEntry<IDomainEvent> entry in entryList)
            entry.Entity.ClearEvents();
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Bid bid)
    {
        if (_context.Entry(bid).State is not EntityState.Modified)
            _context.Bids.Update(bid);

        return await Task.FromResult(true);
    }
}