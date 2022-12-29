using System.Text.Json.Serialization;
using Domain.Models;

namespace BackendPractice.View {
public class UserView {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }
    
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }
    
    [JsonPropertyName("role")]
    public Role Role { get; set; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    }
}