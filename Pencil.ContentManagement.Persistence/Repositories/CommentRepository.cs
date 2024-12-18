using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pencil.ContentManagement.Application.Contracts.Persistence;
using Pencil.ContentManagement.Application.Features.Comments.Queries.GetComments;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    private readonly IMapper _mapper;

    public CommentRepository(PencilDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CommentsDto>> GetAllDtosAsync(Expression<Func<Comment, bool>> wherePredicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Comment>().Where(wherePredicate)
            .Include(c => c.Author)
            .Include(c => c.Likes)
            .Select(c => _mapper.Map<CommentsDto>(c))
            .ToListAsync(cancellationToken);
    }

    public async Task SoftDelete(Comment entity, CancellationToken cancellationToken = default)
    {
        entity.SoftDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}