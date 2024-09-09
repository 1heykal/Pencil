using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class LikePostConfiguration : IEntityTypeConfiguration<Like<Post>>
{
    public void Configure(EntityTypeBuilder<Like<Post>> builder)
    {
        builder
            .HasKey(l => new { l.ItemId, l.UserId });
    }
}
