using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder
            .HasKey(s => s.Id);

        builder
            .HasOne(s => s.Blog)
            .WithMany(b => b.Subscriptions)
            .HasForeignKey(s => s.BlogId)
            .OnDelete(DeleteBehavior.Restrict);


        builder
            .HasQueryFilter(s => s.Active);
    }
}