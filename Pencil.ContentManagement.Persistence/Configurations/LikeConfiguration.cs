using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class LikeConfiguration<T>: IEntityTypeConfiguration<Like<T>>
{
    public void Configure(EntityTypeBuilder<Like<T>> builder)
    {
        builder
            .HasKey(l => new { l.ItemId, l.UserId });
    }
}