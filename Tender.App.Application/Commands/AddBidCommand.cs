using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.Commands;

public record AddBidCommand(
    string Title,
    string Description,
    DateTime StartIn,
    DateTime EndIn,
    long MinAmount,
    long MaxAmount) : IRequest<ResultHandler<BidDto>>;
