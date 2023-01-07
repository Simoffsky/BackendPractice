using Domain.Models;

namespace Domain.Services; 

public class UserService {
    private readonly IUserRepository _repository;
    
    public UserService(IUserRepository repository) {
        _repository = repository;
    }
    public async Task<Result<User>> CreateUser(User user) {
        if (!_repository.IsValid(user))
            return Result.Fail<User>("User data is not valid");
        
        if (await _repository.ExistLogin(user.Username))
            return Result.Fail<User>("User with that username already exists");

        await _repository.Create(user);
        return Result.Ok<User>(user);
    }

    public async Task<Result<User>> GetByLogin(string login) {
        if (string.IsNullOrEmpty(login))
            return Result.Fail<User>("Empty login");
        
        if (!await _repository.ExistLogin(login))
            return Result.Fail<User>("User with this login doesn't exists");

        return Result.Ok<User>(await _repository.GetByLogin(login));

    }

    public async Task<Result<bool>> CheckExist(string login, string password) {
        if (string.IsNullOrEmpty(login))
            return Result.Fail<bool>("Empty login");
        
        if (string.IsNullOrEmpty(password))
            return Result.Fail<bool>("Empty password");


        return Result.Ok<bool>(await _repository.ExistLogin(login, password));
    }
}