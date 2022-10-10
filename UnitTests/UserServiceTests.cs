using Domain.Models;
using Moq;
namespace UnitTests; 

public class UserServiceTests {
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _repository;

    public UserServiceTests () {
        _repository = new Mock<IUserRepository>();
        _userService = new UserService(_repository.Object);
    }

    public User GetUser(string username) {
        return new User(username, "123", 1, "+791452352", "Неважно", Role.Patient);
    }
    
    
    [Fact]
    public void LoginIsEmptyOrNull() {
        var response = _userService.GetByLogin(string.Empty);
        Assert.False(response.Success);
        Assert.Equal("Empty login", response.Error);
    }

    [Fact]
    public void LoginNotFound() {
        _repository.Setup(repo => repo.GetByLogin(It.IsAny<string>()))
            .Returns(() => null);
        
        var response = _userService.GetByLogin("aboba");
        
        Assert.False(response.Success);
        Assert.Equal("User with this login doesn't exists", response.Error);
    }
    [Fact]
    public void LoginFound() {
        _repository.Setup(repo => repo.IsExist(It.Is<string>(s => s == "aboba")))
            .Returns(true);
        _repository.Setup(repo => repo.GetByLogin(It.Is<string>(s => s=="aboba")))
            .Returns(GetUser("aboba"));

        var response = _userService.GetByLogin("aboba");
        
        Assert.True(response.Success);
    }

    [Fact]
    public void CreateAlreadyExists() {
        _repository.Setup(repo => repo.IsExist(It.Is<string>(s => s == "aboba")))
            .Returns(true);

        _repository.Setup(repo => repo.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userService.CreateUser(GetUser("aboba"));
        
        Assert.False(response.Success);
        Assert.Equal("User with that username already exists", response.Error);
    }

    [Fact]
    public void CreateEmptyUsername() {
        _repository.Setup(repo => repo.IsValid(It.Is<User>(user => string.IsNullOrEmpty(user.Username))))
            .Returns(false);
        
        var response = _userService.CreateUser(GetUser(""));
        
        Assert.False(response.Success);
        Assert.Equal("User data is not valid", response.Error);
    }
    
    [Fact]
    public void CreateOk() {
        _repository.Setup(repo => repo.IsExist(It.IsAny<string>()))
            .Returns(false);
        _repository.Setup(repo => repo.IsValid(It.IsAny<User>()))
            .Returns(true);

        var response = _userService.CreateUser(GetUser("aboba"));
        
        Assert.True(response.Success);
    }

    [Fact]
    public void CheckExistEmptyLoginPassword() {
        var response = _userService.CheckExist("", "");
        Assert.False(response.Success);
        Assert.Equal("Empty login/password", response.Error);
        
    }
    
    [Fact]
    public void CheckExistLoginPasswordOk() {
        _repository.Setup(repo => repo.IsExist(
                It.Is<string>(u => u == "aboba"),
                It.Is<string>(p => p == "123")
            )
        ).Returns(true);
        
        var response = _userService.CheckExist("aboba", "123");
        
        Assert.True(response.Success);
    }
    
    
    
    
    
    
}