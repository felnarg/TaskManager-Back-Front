namespace Application.Interfaces
{
    public interface ITaskRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task SaveEntityAsync(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
