namespace Domain.Models; 

public class Schedule {
    public int DoctorId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public Schedule(int doctorId, DateTime startTime, DateTime endTime) {
    DoctorId = doctorId;
        StartTime = startTime;
        EndTime = endTime;
    }
}