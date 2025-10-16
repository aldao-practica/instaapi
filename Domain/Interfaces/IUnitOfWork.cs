namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable // IDsisposable para liberar recursos, Permite usar el patrón using
    //using (var unitOfWork = new UnitOfWork())
    //{
        // usás la conexión a BD
    //} // ← Acá se llama automáticamente Dispose() y cierra la conexión
    //¿Qué es el patrón Unit of Work?
    //Unit of Work agrupa múltiples operaciones en una transacción única.
    // ✅ TODO o NADA
    //using var unitOfWork = new UnitOfWork();
    //    await unitOfWork.Users.AddAsync(user);      // Se prepara
    //    await unitOfWork.Posts.AddAsync(post);      // Se prepara
    //    await unitOfWork.SaveChangesAsync();        // Acá se guarda TODO junto
    // Si algo falla, NADA se guarda (rollback automático)

    {
        IUserRepository Users { get; }
        // Resto de repositorios al que querramos acceder...
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
