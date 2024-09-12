using Microsoft.EntityFrameworkCore;

namespace Pencil.ContentManagement.Persistence;

public class PencilDbContext : DbContext
{
    public PencilDbContext(DbContextOptions<PencilDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PencilDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var foreignKey in entity.GetForeignKeys())
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}