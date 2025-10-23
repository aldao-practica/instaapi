using Domain.Interfaces;

namespace InstaAPI.Mocks;

public class MockUnitOfWork : IUnitOfWork
{
    public IUserRepository Users { get; }
    public IPostRepository Posts => throw new NotImplementedException();

    public MockUnitOfWork(MockUserRepository userRepository)
    {
        Users = userRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(1);
    }

    public void Dispose()
    {
    }
}
