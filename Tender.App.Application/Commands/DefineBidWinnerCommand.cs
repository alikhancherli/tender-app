using MediatR;
using Tender.App.Application.DTOs;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.Commands;

public record DefineBidWinnerCommand(int Id, int BidDetailId) : IRequest<ResultHandler<BidDetailsDto>>;
