namespace Domain.Models; 

public interface IRepository<T> where T: class {

    public Task<IEnumerable<T>> List();
    public Task<T> Create(T item);
    public Task<T> Get(int id);

    public Task<bool> Exists(int id);
    public Task<bool> Delete(int id);

    public bool IsValid(T entity);
    public Task<T> Update(T entity);
}