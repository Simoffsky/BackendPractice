using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackendPractice.Controllers; 

[ApiController]
[Route("token")]
public class AuthController: ControllerBase { 
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;

    public AuthController(IConfiguration config, UserService service) {
        _configuration = config;
        _userService = service;
    }
    
    [HttpPost] 
    public async Task<IActionResult> CreateToken(string login, string password) {

        var res = await _userService.CheckExist(login, password);
        if (!res.Success)
            return Problem(statusCode: 404, detail: res.Error);

        var user = (await _userService.GetByLogin(login)).Value;
        
        
        //create claims details based on the user information
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("Username", user.Username),
            new Claim("PhoneNumber", user.PhoneNumber),
            new Claim("Role", user.Role.ToString())
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signIn);

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}