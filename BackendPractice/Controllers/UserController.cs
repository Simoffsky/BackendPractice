using BackendPractice.View;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendPractice.Controllers; 


[ApiController]
[Route("user")]
public class UserController: ControllerBase {
    private readonly UserService _service;
    public UserController(UserService service) {
        _service = service;
    }
    [HttpGet("login")]
    public ActionResult<UserView> GetUserByLogin(string login) {
        if (login == string.Empty)
            return Problem(statusCode: 404, detail: "Login was not provided...");

        var userRes = _service.GetByLogin(login);
        if (!userRes.Success)
            return Problem(statusCode: 404, detail: userRes.Error);
        return Ok(new UserView {
            Id = userRes.Value.Id,
            Username = userRes.Value.Username,
            Password = userRes.Value.Password,
            FullName = userRes.Value.FullName,
            PhoneNumber = userRes.Value.PhoneNumber,
            Role = userRes.Value.Role
        });
    }
    
    [Authorize]
    [HttpPost("CreateUser")]
    public ActionResult<UserView> CreateUser(UserView userView)
    {
        if (string.IsNullOrEmpty(userView.Username)) 
            return Problem(statusCode: 404, detail: "Login is empty or null.");
        if (string.IsNullOrEmpty(userView.Password)) 
            return Problem(statusCode: 404, detail: "Password is empty or null.");


        var user = new User(
            userView.Username,
            userView.Password,
            userView.Id,
            userView.PhoneNumber,
            userView.FullName,
            userView.Role);
        
        var userResult = _service.CreateUser(user);
        
         if (!userResult.Success)
             return Problem(statusCode: 400, detail: userResult.Error);
        
        return Ok(userView);
    }
    
    [HttpGet("exists")]
    public ActionResult<UserView> IsUserExists(string login, string password) {
        var res = _service.CheckExist(login, password);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);
        return Ok(res.Value);
    }
}