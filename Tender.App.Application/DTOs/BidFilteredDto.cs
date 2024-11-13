using Tender.App.Domain.Shared;

namespace Tender.App.Application.DTOs;

public record BidFilteredDto(
    string Title,
    string Description,
    DateTime StartIn,
    DateTime EndIn,
    long MinAmount,
    long MaxAmount,
    int ContributorsCount,
    IEnumerable<BidDetailsDto> BidDetails,
    bool IsActive,
    long LowestAmount)
    :BaseDto<int>;
