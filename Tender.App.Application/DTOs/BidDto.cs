using Tender.App.Domain.Shared;
using Tender.App.Domain.ValueObjects;

namespace Tender.App.Application.DTOs;

public record BidDto(
    string Title,
    string Description,
    DateTime StartIn,
    DateTime EndIn,
    long MinAmount,
    long MaxAmount,
    IEnumerable<BidDetailsDto> BidDetails,
    bool IsActive) : BaseDto<int>;
