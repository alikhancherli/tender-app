using Tender.App.Domain.Shared;
using Tender.App.Domain.ValueObjects;

namespace Tender.App.Application.DTOs;

public record BidDetailsDto(
    int UserId,
    long Amount,
    bool IsWinner) : BaseDto<int>;
