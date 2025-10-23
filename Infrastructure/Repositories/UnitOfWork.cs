using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository? _users;
    private IPostRepository? _posts;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IPostRepository Posts => _posts ??= new PostRepository(_context);
    //Lazy initialization: Solo se crea el repositorio cuando se usa por primera vez
    //??= es el operador de asignación con null-coalescing
    //Si _users es null, lo crea; si no, devuelve el existente

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
