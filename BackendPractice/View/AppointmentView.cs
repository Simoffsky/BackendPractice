using System.Text.Json.Serialization;

namespace BackendPractice.View; 

public class AppointmentView {
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }
    
    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }
    
    [JsonPropertyName("patient_id")]
    public int PatientId { get; set; }
    
    [JsonPropertyName("doctor_id")]
    public int DoctorId { get; set; }
}