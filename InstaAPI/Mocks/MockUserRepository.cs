using Domain.Entities;
using Domain.Interfaces;

namespace InstaAPI.Mocks;

public class MockUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var mockUser = User.Create(
            username: "juancito_test",
            email: "juan@test.com",
            passwordHash: BCrypt.Net.BCrypt.HashPassword("Password123")
        );

        return Task.FromResult<User?>(mockUser);

    }

    public Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<User>>(_users);
    }

    public Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
    {
        _users.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(User entity, CancellationToken cancellationToken = default)
    {
        var existing = _users.FirstOrDefault(u => u.Id == entity.Id);
        if (existing != null)
        {
            _users.Remove(existing);
            _users.Add(entity);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        _users.Remove(entity);
        return Task.CompletedTask;
    }

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        return Task.FromResult(user);
    }

    public Task<bool> ExistsAsync(string email, string username, CancellationToken cancellationToken = default)
    {
        var exists = _users.Any(u => u.Email == email || u.Username == username);
        return Task.FromResult(exists);
    }
}
