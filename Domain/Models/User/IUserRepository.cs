namespace Domain.Models;
public interface IUserRepository : IRepository<User> {
    bool ExistLogin(string login, string password);
    bool ExistLogin(string login);
    User? GetByLogin(string login);
    
}