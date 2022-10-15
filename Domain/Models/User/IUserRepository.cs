namespace Domain.Models;
public interface IUserRepository : IRepository<User> {
    
    
    bool ExistLogin(string login, string password);

    bool ExistLogin(string login);
    bool IsValid(User user);
    User GetByLogin(string login);
    

}