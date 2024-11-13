using Tender.App.Domain.Entities;
using Tender.App.Domain.Shared;

namespace Tender.App.Domain.Events;

public sealed record BidAddedEvent(Bid Bid):IDomainEventFlag;
