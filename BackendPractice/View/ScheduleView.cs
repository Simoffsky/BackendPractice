using System.Text.Json.Serialization;

namespace BackendPractice.View; 

public class ScheduleView {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("doctor_id")]
    public int DoctorId { get; set; }
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }
}