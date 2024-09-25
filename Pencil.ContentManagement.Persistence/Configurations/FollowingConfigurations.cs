using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class FollowingConfigurations : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder
            .HasIndex(f => new { f.FollowedId, f.FollowerId })
            .IsUnique();

        builder
            .HasQueryFilter(f => !f.SoftDeleted);
    }
}