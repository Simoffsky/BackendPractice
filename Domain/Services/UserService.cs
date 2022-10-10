using Domain.Models;

namespace Domain.Services; 

public class UserService {
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository) {
        _repository = repository;
    }
    public Result<User> CreateUser(User user) {
        if (_repository.IsExist(user.Username))
            return Result.Fail<User>("User with that username already exists");

        if (!_repository.IsValid(user))
            return Result.Fail<User>("User data is not valid");
        return Result.Ok<User>(user);
    }

    public Result<User> GetByLogin(string login) {
        if (!_repository.IsExist(login))
            return Result.Fail<User>("User with this login doesn't exists");

        return Result.Ok<User>(_repository.GetByLogin(login));

    }

    public Result<bool> CheckExist(string login, string password) {
        if (login == string.Empty || password == string.Empty)
            return Result.Fail<bool>("Empty login/password");

        return Result.Ok<bool>(_repository.IsExist(login, password));
    }
}