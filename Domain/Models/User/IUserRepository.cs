namespace Domain.Models;
public interface IUserRepository : IRepository<User> {
    bool IsUserExist(string login);
    User GetByLogin(string login);

}