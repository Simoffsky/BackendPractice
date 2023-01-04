using System.Text.Json.Serialization;
using Domain.Models;

namespace BackendPractice.View;
public class DoctorView
{
    [JsonPropertyName("id")]
    public int DoctorId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("specialization")]
    public Specialization Specialization { get; set; }
}