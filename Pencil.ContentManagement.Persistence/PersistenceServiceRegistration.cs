using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Persistence.Repositories;

namespace Pencil.ContentManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PencilDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("PencilContentManagementConnectionString"));
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IFollowingRepository, FollowingRepository>();
        services.AddScoped<ILikePostRepository, LikePostRepository>();
        services.AddScoped<ILikeCommentRepository, LikeCommentRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        return services;
    }
}