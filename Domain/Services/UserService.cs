using Domain.Models;

namespace Domain.Services; 

public class UserService {
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository) {
        _repository = repository;
    }
    public Result<User> CreateUser(User user) {
        if (!_repository.IsValid(user))
            return Result.Fail<User>("User data is not valid");
        
        if (_repository.ExistLogin(user.Username))
            return Result.Fail<User>("User with that username already exists");

        _repository.Create(user);
        return Result.Ok<User>(user);
    }

    public Result<User> GetByLogin(string login) {
        if (string.IsNullOrEmpty(login))
            return Result.Fail<User>("Empty login");
        
        if (!_repository.ExistLogin(login))
            return Result.Fail<User>("User with this login doesn't exists");

        return Result.Ok<User>(_repository.GetByLogin(login));

    }

    public Result<bool> CheckExist(string login, string password) {
        if (string.IsNullOrEmpty(login))
            return Result.Fail<bool>("Empty login");
        
        if (string.IsNullOrEmpty(password))
            return Result.Fail<bool>("Empty password");


        return Result.Ok<bool>(_repository.ExistLogin(login, password));
    }
}