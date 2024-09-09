using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasKey(a => a.Id);
        
        builder
            .HasIndex(a => a.Username)
            .IsUnique();

        builder
            .HasIndex(a => a.Email)
            .IsUnique();

        builder
            .HasMany(a => a.Following)
            .WithOne(f => f.Follower)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(a => a.Followers)
            .WithOne(f => f.Followed)
            .HasForeignKey(f => f.FollowedId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasQueryFilter(a => !a.SoftDeleted);
    }
}