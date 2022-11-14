namespace Domain.Models; 

public interface IRepository<T> where T: class {

    public IEnumerable<T> List();
    public T Create(T item);
    public T? Get(int id);

    public bool Exists(int id);
    public bool Delete(int id);

    public bool IsValid(T entity);
    public T Update(T entity);
}