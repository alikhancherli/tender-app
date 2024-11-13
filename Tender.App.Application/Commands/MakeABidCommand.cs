using MediatR;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.Commands;

public record MakeABidCommand(int BidId, int UserId, long Amount) : IRequest<ResultHandler<bool>>;
