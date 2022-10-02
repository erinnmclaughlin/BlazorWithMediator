namespace BlazorWithMediator.Shared.Common;

public interface IRepository<T>
{
    Task<List<T>> GetAll(CancellationToken ct);
    Task<T?> GetById(int id, CancellationToken ct);

    Task<int> Create(T item, CancellationToken ct);
    Task Update(T item, CancellationToken ct);
    Task Delete(int id, CancellationToken ct);
}
