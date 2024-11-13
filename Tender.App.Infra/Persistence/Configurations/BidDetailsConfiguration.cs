using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tender.App.Domain.Entities;
using Tender.App.Domain.ValueObjects;

namespace Tender.App.Infra.Persistence.Configurations;

public sealed class BidDetailsConfiguration : IEntityTypeConfiguration<BidDetails>
{
    public void Configure(EntityTypeBuilder<BidDetails> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasConversion(src => src.Value, dest => new AmountValueObject(dest));


        builder.Property(x => x.UserId).IsRequired();
    }
}
