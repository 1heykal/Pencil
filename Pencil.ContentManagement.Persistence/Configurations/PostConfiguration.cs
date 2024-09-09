using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasKey(p => p.Id);
        
        builder
            .HasIndex(p => p.Url)
            .IsUnique();

        builder
            .HasQueryFilter(p => !p.SoftDeleted);

    }
}