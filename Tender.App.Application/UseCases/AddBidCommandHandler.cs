using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Tender.App.Application.Commands;
using Tender.App.Application.DTOs;
using Tender.App.Domain.Entities;
using Tender.App.Domain.Repositories;
using Tender.App.Domain.Shared;

namespace Tender.App.Application.UseCases;

public sealed class AddBidCommandHandler(IBidRepository bidRepository, IValidator<AddBidCommand> validator)
    : IRequestHandler<AddBidCommand, ResultHandler<BidDto>>
{
    public async Task<ResultHandler<BidDto>> Handle(AddBidCommand request, CancellationToken cancellationToken)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid) return ResultHandler<BidDto>.Failure(string.Join(" , ", validation.Errors));

        var bid = new Bid(
            request.Title,
            request.Description,
            request.StartIn,
            request.EndIn,
            request.MinAmount,
            request.MaxAmount);

        await bidRepository.AddAsync(bid);
        await bidRepository.SaveAndDispatchEventsAsync(cancellationToken);

        var mappedModel = bid.Adapt<BidDto>();

        return ResultHandler<BidDto>.Success(mappedModel);
    }
}
