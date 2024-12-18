using System.Linq.Expressions;
using Pencil.ContentManagement.Application.Features.Boxes.Queries;
using Pencil.ContentManagement.Domain.Entities;

namespace Pencil.ContentManagement.Application.Contracts.Persistence;

public interface IBoxRepository : IAsyncRepository<Box>
{
    Task<BoxDto?> GetBoxDtoByUserIdAsync(Guid id, CancellationToken cancellationToken = default);
}