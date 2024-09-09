using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Configurations;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder
            .HasKey(b => b.Id);
        
        builder
            .HasIndex(b => b.Username)
            .IsUnique();

        builder
            .HasQueryFilter(b => !b.SoftDeleted);
    }
}