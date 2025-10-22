namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
        // T es un tipo generico, en la implementacion reemplazamos T por la entidad concreta
        // (como está el where T : class solo puede ser clases, no int, bool etc), usar genericos evita que tengamos codigo repetido, sino tendriamos que
        // usar un IUserRepository, IPostRepository, ICommentRepository etc

        //¿Por qué las interfaces en Domain?

        //Dependency Inversion (SOLID): Domain define QUÉ necesita, Infrastructure implementa CÓMO
        //Domain no depende de nadie (ni de Entity Framework, ni de base de datos específica)
        //Podés cambiar de SQL Server a PostgreSQL sin tocar Domain
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
