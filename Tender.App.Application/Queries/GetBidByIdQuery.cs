using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.Queries;

public record GetBidByIdQuery(int Id) : IRequest<ResultHandler<BidDto>>;
