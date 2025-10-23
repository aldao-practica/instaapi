using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Post>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet.Include(p => p.User).Where(p => p.UserId == userId).OrderByDescending(p => p.CreatedAt).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Post>> GetFeedAsync(int skip, int take, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(p => p.User)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }
}
