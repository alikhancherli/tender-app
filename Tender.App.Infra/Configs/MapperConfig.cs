using Mapster;
using Tender.App.Application.DTOs;
using Tender.App.Domain.Entities;

namespace Tender.App.Infra.Configs;

public static class MapperConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Bid, BidDto>
            .NewConfig()
            .MapWith(src => new BidDto(
                src.Title,
                src.Description,
                src.StartIn,
                src.EndIn,
                src.MinAmount.Value,
                src.MaxAmount.Value,
                src.BidDetails.Select(g => new BidDetailsDto(g.UserId, g.Amount.Value, g.IsWinner)
                {
                    Id = g.Id,
                    CreatedTimeUtc = g.CreatedTimeUtc,
                    ModifiedTimeUtc = g.ModifiedTimeUtc
                }).ToList(),
                src.IsActive())
            {
                Id = src.Id,
                CreatedTimeUtc = src.CreatedTimeUtc,
                ModifiedTimeUtc = src.ModifiedTimeUtc
            });

        TypeAdapterConfig<Bid, BidFilteredDto>
            .NewConfig()
            .MapWith(src => new BidFilteredDto(
                src.Title,
                src.Description,
                src.StartIn,
                src.EndIn,
                src.MinAmount.Value,
                src.MaxAmount.Value,
                src.BidDetails.Count(),
                src.BidDetails.Select(g => new BidDetailsDto(g.UserId, g.Amount.Value, g.IsWinner)
                {
                    Id = g.Id,
                    CreatedTimeUtc = g.CreatedTimeUtc,
                    ModifiedTimeUtc = g.ModifiedTimeUtc
                }).ToList(),
                src.IsActive(),
                src.BidDetails.Min(x => x.Amount.Value))
            {
                Id = src.Id,
                CreatedTimeUtc = src.CreatedTimeUtc,
                ModifiedTimeUtc = src.ModifiedTimeUtc
            });

        TypeAdapterConfig<BidDetails, BidDetailsDto>
            .NewConfig()
            .MapWith(src => new BidDetailsDto(
                src.UserId,
                src.Amount.Value,
                src.IsWinner)
            {
                Id = src.Id,
                CreatedTimeUtc = src.CreatedTimeUtc,
                ModifiedTimeUtc = src.ModifiedTimeUtc
            });
    }
}
