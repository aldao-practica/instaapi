using Domain.Entities;

namespace Domain.Interfaces;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default); // default: no cancela nada, así que la operación sigue su curso normal hasta terminar.
    Task<IEnumerable<Post>> GetFeedAsync(int skip, int take, CancellationToken cancellationToken = default);
}
