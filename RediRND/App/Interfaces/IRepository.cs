namespace RediRND.App.Interfaces;

public interface IRepository<T> where T : IAggregateRoot
{
    public Task AddAsync(T entity);
    public Task AddRangeAsync(IEnumerable<T> entities);
    public Task DeleteByIdAsync(int id);
    public Task Exists(int id);
    public Task<T> GetByIdAsync(int id);
    public Task<List<T>> GetAllAsync();
    public Task UpdateAsync(T entity);
    public Task UpdateRangeAsync(IEnumerable<T> entities);
}

