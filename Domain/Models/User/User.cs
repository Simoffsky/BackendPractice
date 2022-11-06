namespace Domain.Models;

public class User {
    public string Username;
    public string Password;
    public int Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }


    public User(string username, string password, int id, string phoneNumber, string fullName, Role role) {
        Username = username;
        Password = password;
        Id = id;
        PhoneNumber = phoneNumber;
        FullName = fullName;
        Role = role;
    }

}