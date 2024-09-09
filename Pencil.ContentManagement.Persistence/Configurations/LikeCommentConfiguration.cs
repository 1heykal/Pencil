using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class LikeCommentConfiguration : IEntityTypeConfiguration<Like<Comment>>
{
    public void Configure(EntityTypeBuilder<Like<Comment>> builder)
    {
        builder
            .HasKey(l => new { l.ItemId, l.UserId });
    }
}
