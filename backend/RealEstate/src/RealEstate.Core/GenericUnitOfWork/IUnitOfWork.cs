using RealEstate.Core.GenericRepositories;

namespace RealEstate.Core.GenericUnitOfWork;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
    Task<int> SaveChangesAsync();
    T Repository<T>() where T : IBaseRepository;
    Task BeginWritingAsync(CancellationToken cancellationToken);
    Task CommitWritingsAsync(CancellationToken cancellationToken);
    Task DiscardWritingsAsync(CancellationToken cancellationToken);
    /// <summary>
    /// True ise transaction başlatılmış, false ise transaction başlatılmamıştır.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    bool HasTransaction();

}

