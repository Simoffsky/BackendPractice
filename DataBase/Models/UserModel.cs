using Domain.Models;
namespace DataBase.Models; 

public class UserModel {
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }
}