using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Boxes.Queries;
using Pencil.ContentManagement.Application.Features.Posts.Queries.GetPosts;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class BoxRepository : BaseRepository<Box>, IBoxRepository
{
    private IMapper _mapper;
    public BoxRepository(PencilDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override async Task<Box?> GetAsync(Expression<Func<Box, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Box>().Where(wherePredicate)
            .Include(b => b.Posts)
            .ThenInclude(p => p.Author)
            .Include(b => b.Posts)
            .ThenInclude(p => p.Likes)
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<BoxDto?> GetBoxDtoByUserIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        
        
        var box = await _dbContext.Set<Box>().Where(b => b.CreatorId.Equals(id))
            .Include(b => b.Posts)
            .ThenInclude(p => p.Author)
            .Include(b => b.Posts)
            .ThenInclude(p => p.Likes)
            .FirstOrDefaultAsync(cancellationToken);

        var likedPostsIds = _dbContext.Set<Like<Post>>()
            .Where(u => u.UserId.Equals(id))
            .AsNoTracking()
            .Select(l => l.ItemId);

     
       var boxDto = _mapper.Map<BoxDto>(box);

        foreach (var post in boxDto.Posts)
        {
            post.Liked = await likedPostsIds.ContainsAsync(post.Id, cancellationToken);
            post.Saved = true;
        }

        boxDto = boxDto with {Posts = boxDto.Posts.OrderByDescending(p => p.PublishedOn).ToList()};
        
        return boxDto;
    }
}