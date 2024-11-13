using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tender.App.Domain.Entities;
using Tender.App.Domain.ValueObjects;

namespace Tender.App.Infra.Persistence.Configurations;

public sealed class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.MinAmount)
            .IsRequired()
            .HasConversion(src => src.Value, dest => new AmountValueObject(dest));

        builder.Property(x => x.MaxAmount)
            .IsRequired()
            .HasConversion(src => src.Value, dest => new AmountValueObject(dest));

        builder.Property(x => x.StartIn).IsRequired();
        builder.Property(x => x.EndIn).IsRequired();

        builder.HasMany(x=>x.BidDetails)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
